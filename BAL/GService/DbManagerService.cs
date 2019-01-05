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
using R.BusinessEntities.Manager;

namespace R.BAL
{
    public class DbManagerService : iDbManagerService
    {
        private readonly cUnitOfWork _unitOfWork;
        public DbManagerService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        //public DbManagerEntity GetById(string tid)
        //{
        //    var result = _unitOfWork.ManagerRepository.Get(t=>t.mguid==tid);
        //    if (result != null)
        //    {
        //        var resultModel = GsMapperConfig.DbManagerEntityMapper(result);
        //        return resultModel;
        //    }
        //    return null;
        //}
        public DbManagerEntity GetById(long tid)
        {
            var result = _unitOfWork.ManagerRepository.GetByID(tid);
            if (result != null)
            {
                var resultModel = GsMapperConfig.DbManagerEntityMapper(result);
                return resultModel;
            }
            return null;
        }
        public IEnumerable<DbManagerEntity> GetAll()
        {
            var results = _unitOfWork.ManagerRepository.GetAll().ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.DbManagerEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }

        public IEnumerable<DbManagerEntity> GetByType(string type)
        {
            var results = _unitOfWork.ManagerRepository.GetMany(t => t.mrowtype == type).ToList();
            if (results.Any())
            {
                var resultsmodel = GsMapperConfig.DbManagerEntityMapperList(results);
                return resultsmodel;
            }
            return null;
        }
        public long Create(DbManagerEntity tentity)
        {
            using (var scope = new TransactionScope())
            {
                var NewRecord = new Ta_Manager
                {
                    //mguid = tentity.mguid,
                    mcontent = tentity.mcontent,
                    mrowtype = tentity.mrowtype,
                };
                _unitOfWork.ManagerRepository.Insert(NewRecord);
                _unitOfWork.Save();
                scope.Complete();
                return NewRecord.mid;
            }
        }
        public bool Update(long tid, DbManagerEntity tentity)
        {
            var success = false;
            if (tentity != null && tid != 0)
            {
                using (var scope = new TransactionScope())
                {
                    var oldrecord = _unitOfWork.ManagerRepository.GetByID(tid);
                    if (oldrecord != null)
                    {
                        oldrecord.mcontent = tentity.mcontent;

                        _unitOfWork.ManagerRepository.Update(oldrecord);
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
                    var result = _unitOfWork.ManagerRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.ManagerRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        public IEnumerable<DbManagerEntity> GetAllByOther(long otherid)
        {
            throw new NotImplementedException();
        }
    }
} 
