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
    public class TagsModuleServiceG : iTagsModuleService
    {
        private readonly cUnitOfWork _unitOfWork;

        public TagsModuleServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public TagsModel GetById(long tid)
        {
            var result = _unitOfWork.TagsRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.TagsModelEntityMapper(result);
                return resultModel;
            }
            return null;
        }

        public TagsModel GetByName(string tid)
        {
            var result = _unitOfWork.TagsRepository.Get(a=>a.tagname==tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.TagsModelEntityMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<TagsModel> GetAll()
        {
            var results = _unitOfWork.TagsRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.TagsModelEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(TagsModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_blog_tags
                {
                    tagname = tentity.tagname,
                };
                _unitOfWork.TagsRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.tagid;
            }
        }

        public bool Update(long tid, TagsModel tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.TagsRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.tagname = tentity.tagname;

                        _unitOfWork.TagsRepository.Update(oldrecord);
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
                    var result = _unitOfWork.TagsRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.TagsRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }





        public IEnumerable<TagsModel> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagsModel> GetAllByOther(string[] otherid)
        {
            var results = _unitOfWork.TagsRepository.GetMany(a=> otherid.Contains(a.tagid.ToString())).ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.TagsModelEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }
    }
}
