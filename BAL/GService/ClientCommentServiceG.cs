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
    public class ClientCommentServiceG : iClientCommentService
    {
        private readonly cUnitOfWork _unitOfWork;

        public ClientCommentServiceG(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public ClientComments GetById(long tid)
        {
            var result = _unitOfWork.ClientCommentRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.ClientCommentMapper(result);
                return resultModel;
            }
            return null;
        }

        public IEnumerable<ClientComments> GetAll()
        {
            var results = _unitOfWork.ClientCommentRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.ClientCommentMapperList(results);
                return resultsmodel;
            }
            return null;
        }


        public long Create(ClientComments tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new ta_ussbk_client_comment
                {
                    cmntcontent = tentity.cmntcontent,
                    cmntcreatedate = tentity.cmntcreatedate,
                    cmntdate = tentity.cmntdate,
                    cmntype = tentity.cmntype,
                    userfkid = tentity.userfkid,
                };
                _unitOfWork.ClientCommentRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.cmntid;
            }
        }

        public bool Update(long tid, ClientComments tentity)
        {
            var success = false;
            if (tentity != null && tid!=0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.ClientCommentRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.cmntcontent = tentity.cmntcontent;
                        oldrecord.cmntcreatedate = tentity.cmntcreatedate;
                        oldrecord.cmntdate = tentity.cmntdate;
                        oldrecord.cmntype = tentity.cmntype;
                        oldrecord.userfkid = tentity.userfkid;

                        _unitOfWork.ClientCommentRepository.Update(oldrecord);
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
                    var result = _unitOfWork.ClientCommentRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.ClientCommentRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


        public IEnumerable<ClientComments> GetAllByOther(long otherid)
        {
            var results = _unitOfWork.ClientCommentRepository.GetMany(i => i.userfkid == otherid).ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.ClientCommentMapperList(results);
                return resultsmodel;
            }
            return null;
        }

   
       
    }
}
