using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.DAL.UnitOfWork;
using R.BusinessEntities;
using System.Transactions;
using DAL;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Net;

namespace R.BAL 
{
    public class BlogsModuleServiceG : iBlogModuleService
    {
        private readonly cUnitOfWork _unitOfWork;

        public BlogsModuleServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public BlogModuleModel GetById(long tid)
        {
            var result = _unitOfWork.BlogsRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.BlogEntityMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<BlogModuleModel> GetAll()
        {
            var results = _unitOfWork.BlogsRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.BlogEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(BlogModuleModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_blog
                {
                    blogdate = tentity.blogdate,
                    blogdescription = tentity.blogdescription,
                    blogimage = tentity.blogimage,
                    blogkeywords = tentity.blogkeywords,
                    blogtags = tentity.blogtags,
                    blogtitle = tentity.blogtitle,
                    blogupdate = tentity.blogupdate,
                    blogwebsite = tentity.blogwebsite,
                    blogpublished=tentity.blogpublished,
                };
                
                
                _unitOfWork.BlogsRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.blogid;
            }
        }

        public bool Update(long tid, BlogModuleModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.BlogsRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.blogdate = tentity.blogdate;
                        oldrecord.blogdescription = tentity.blogdescription;
                        oldrecord.blogimage = tentity.blogimage;
                        oldrecord.blogkeywords = tentity.blogkeywords;
                        oldrecord.blogtags = tentity.blogtags;
                        oldrecord.blogtitle = tentity.blogtitle;
                        oldrecord.blogupdate = tentity.blogupdate;
                        oldrecord.blogwebsite = tentity.blogwebsite;
                        oldrecord.blogpublished = tentity.blogpublished;
                        
                        _unitOfWork.BlogsRepository.Update(oldrecord);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

       

       
       

        public bool Delete(long tid)
        {
            var success = false;
            if (tid > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var result = _unitOfWork.BlogsRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.BlogsRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





      

         IEnumerable<BlogModuleModel> iCommon<BlogModuleModel>.GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }

        public BlogModuleModel GetByBlogNameKeyword(string keywords)
        {
            var result = _unitOfWork.BlogsRepository.Get(a => a.blogkeywords==keywords);
            if (result != null)
            {
                var resultModel = GsMapperConfig.BlogEntityMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<BlogModuleModel> GetAllByOther(string[] otherid)
        {
            var results = _unitOfWork.BlogsRepository.GetMany(a => a.blogtags.Contains(otherid.ToString())).ToList();
            //var results = _unitOfWork.BlogsRepository.GetMany(a => otherid.Contains(a.blogtags.ToString())).ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.BlogEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }

        
        
    }
}
