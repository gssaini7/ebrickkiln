using DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DAL;

namespace R.DAL.UnitOfWork
{
    public class cUnitOfWork : IDisposable, iUnitOfWork
    {
        #region Private member variables...
        
        private ussdbEntities _context = null;

        private Genericrepository<ta_ussbk_categoryMaster> _ta_ussbk_categoryMaster;
        private Genericrepository<ta_ussbk_clientrecord> _ta_ussbk_clientrecord;
        private Genericrepository<ta_ussbk_Description> _ta_ussbk_Description;
        private Genericrepository<ta_ussbk_TitleMaster> _ta_ussbk_TitleMaster;
        private Genericrepository<TA_ussbk_Coupons> _TA_ussbk_Coupons;

        private Genericrepository<ta_ussbk_ProjectDetail> _ta_ussbk_ProjectDetail;
        private Genericrepository<Ta_Manager> _Ta_Manager;
        private Genericrepository<ta_blog> _ta_blog; 

        private Genericrepository<ta_blog_tags> _ta_blog_tags;
        private Genericrepository<ta_ussbk_client_comment> _ta_ussbk_client_comment;





        #endregion

        public cUnitOfWork()
        {

            _context = new ussdbEntities();
        }

        #region Public Repository Creation properties...

         ////<summary>
         ////Get/Set Property for product repository.
         ////</summary>
        public Genericrepository<ta_ussbk_categoryMaster> CategotyMasterRepository
        {
            get
            {
                if (this._ta_ussbk_categoryMaster == null)
                    this._ta_ussbk_categoryMaster = new Genericrepository<ta_ussbk_categoryMaster>(_context);
                return _ta_ussbk_categoryMaster;
            }
        }

        public Genericrepository<ta_ussbk_clientrecord> ClientRecordRepository
        {
            get
            {
                if (this._ta_ussbk_clientrecord == null)
                    this._ta_ussbk_clientrecord = new Genericrepository<ta_ussbk_clientrecord>(_context);
                return _ta_ussbk_clientrecord;
            }
        }

        public Genericrepository<ta_ussbk_Description> DescriptionRepository
        {
            get
            {
                if (this._ta_ussbk_Description == null)
                    this._ta_ussbk_Description = new Genericrepository<ta_ussbk_Description>(_context);
                return _ta_ussbk_Description;
            }
        }

        public Genericrepository<ta_ussbk_TitleMaster> TitleMasterRepository
        {
            get
            {
                if (this._ta_ussbk_TitleMaster == null)
                    this._ta_ussbk_TitleMaster = new Genericrepository<ta_ussbk_TitleMaster>(_context);
                return _ta_ussbk_TitleMaster;
            }
        }
        public Genericrepository<TA_ussbk_Coupons> CouponRepository { get { if (this._TA_ussbk_Coupons == null) this._TA_ussbk_Coupons = new Genericrepository<TA_ussbk_Coupons>(_context); return _TA_ussbk_Coupons; } }

        public Genericrepository<ta_ussbk_ProjectDetail> ProjectDetailRepository
        {
            get
            {
                if (this._ta_ussbk_ProjectDetail == null) this._ta_ussbk_ProjectDetail = new Genericrepository<ta_ussbk_ProjectDetail>(_context);
                return _ta_ussbk_ProjectDetail;
            }
        }
        public Genericrepository<Ta_Manager> ManagerRepository
        {
            get
            {
                if (this._Ta_Manager == null) this._Ta_Manager = new Genericrepository<Ta_Manager>(_context);
                return _Ta_Manager;
            }
        }
        public Genericrepository<ta_blog_tags> TagsRepository
        {
            get
            {
                if (this._ta_blog_tags == null) this._ta_blog_tags = new Genericrepository<ta_blog_tags>(_context);
                return _ta_blog_tags;
            }
        }
        public Genericrepository<ta_blog> BlogsRepository
        {
            get
            {
                if (this._ta_blog == null) this._ta_blog = new Genericrepository<ta_blog>(_context);
                return _ta_blog;
            }
        }

        public Genericrepository<ta_ussbk_client_comment> ClientCommentRepository
        {
            get
            {
                if (this._ta_ussbk_client_comment == null) this._ta_ussbk_client_comment = new Genericrepository<ta_ussbk_client_comment>(_context);
                return _ta_ussbk_client_comment;
            }
        }
       
        //public Genericrepository<TA_lib_BookMaster> BookMasterRepository
        //{
        //    get
        //    {
        //        if (this._TA_lib_BookMaster == null)
        //            this._TA_lib_BookMaster = new Genericrepository<TA_lib_BookMaster>(_context);
        //        return _TA_lib_BookMaster;
        //    }
        //}

        //public Genericrepository<ta_vb_Color> ColorRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_ColorRepository == null)
        //            this._ta_vb_ColorRepository = new Genericrepository<ta_vb_Color>(_context);
        //        return _ta_vb_ColorRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_FuelType> FuelTypeRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_FuelTypeRepository == null)
        //            this._ta_vb_FuelTypeRepository = new Genericrepository<ta_vb_FuelType>(_context);
        //        return _ta_vb_FuelTypeRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_KM> KMRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_KMRepository == null)
        //            this._ta_vb_KMRepository = new Genericrepository<ta_vb_KM>(_context);
        //        return _ta_vb_KMRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_make> MakeRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_makeRepository == null)
        //            this._ta_vb_makeRepository = new Genericrepository<ta_vb_make>(_context);
        //        return _ta_vb_makeRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_Model> ModelRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_ModelRepository == null)
        //            this._ta_vb_ModelRepository = new Genericrepository<ta_vb_Model>(_context);
        //        return _ta_vb_ModelRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_Varient> VarientRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_VarientRepository == null)
        //            this._ta_vb_VarientRepository = new Genericrepository<ta_vb_Varient>(_context);
        //        return _ta_vb_VarientRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_VehicleType> VehicleTypeRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_VehicleTypeRepository == null)
        //            this._ta_vb_VehicleTypeRepository = new Genericrepository<ta_vb_VehicleType>(_context);
        //        return _ta_vb_VehicleTypeRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_Image> ImageRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_ImageRepository == null)
        //            this._ta_vb_ImageRepository = new Genericrepository<ta_vb_Image>(_context);
        //        return _ta_vb_ImageRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_imageguid> ImageGUIDRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_ImageGUIDRepository == null)
        //            this._ta_vb_ImageGUIDRepository = new Genericrepository<ta_vb_imageguid>(_context);
        //        return _ta_vb_ImageGUIDRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_advertisement> AdvertisementRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_advertisementRepository == null)
        //            this._ta_vb_advertisementRepository = new Genericrepository<ta_vb_advertisement>(_context);
        //        return _ta_vb_advertisementRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_dealerproperty> DealerPrpertyRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_dealerpropertyRepository == null)
        //            this._ta_vb_dealerpropertyRepository = new Genericrepository<ta_vb_dealerproperty>(_context);
        //        return _ta_vb_dealerpropertyRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_rel_dealervehicle> RelationDealerVehicleRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_rel_dealervehicleRepository == null)
        //            this._ta_vb_rel_dealervehicleRepository = new Genericrepository<ta_vb_rel_dealervehicle>(_context);
        //        return _ta_vb_rel_dealervehicleRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_rel_buyervehicle> RelationBuyerVehicleRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_rel_buyervehicleRepository == null)
        //            this._ta_vb_rel_buyervehicleRepository = new Genericrepository<ta_vb_rel_buyervehicle>(_context);
        //        return _ta_vb_rel_buyervehicleRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_AdminSettings> PublicSettingsRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_AdminSettingsRepository == null)
        //            this._ta_vb_AdminSettingsRepository = new Genericrepository<ta_vb_AdminSettings>(_context);
        //        return _ta_vb_AdminSettingsRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_master_commisssion> CommissionmasterRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_master_commisssionRepository == null)
        //            this._ta_vb_master_commisssionRepository = new Genericrepository<ta_vb_master_commisssion>(_context);
        //        return _ta_vb_master_commisssionRepository;
        //    }
        //}

        //public Genericrepository<ta_vb_sale> SaleRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_saleRepository == null)
        //            this._ta_vb_saleRepository = new Genericrepository<ta_vb_sale>(_context);
        //        return _ta_vb_saleRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_VehicleDealerChecks> DealerCheckRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_VehicleDealerChecksRepository == null)
        //            this._ta_vb_VehicleDealerChecksRepository = new Genericrepository<ta_vb_VehicleDealerChecks>(_context);
        //        return _ta_vb_VehicleDealerChecksRepository;
        //    }
        //}
        //public Genericrepository<ta_vb_partner> PartnerRepository
        //{
        //    get
        //    {
        //        if (this._ta_vb_partnerRepository == null)
        //            this._ta_vb_partnerRepository = new Genericrepository<ta_vb_partner>(_context);
        //        return _ta_vb_partnerRepository;
        //    }
        //}
        

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        ////public Genericrepository<TA_L_Item> ItemRepository
        ////{
        ////    get
        ////    {
        ////        if (this._TA_L_ItemRepository == null)
        ////            this._TA_L_ItemRepository = new Genericrepository<TA_L_Item>(_context);
        ////        return _TA_L_ItemRepository;
        ////    }
        ////}

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        ////public Genericrepository<TA_L_Record> RecordRepository
        ////{
        ////    get
        ////    {
        ////        if (this._TA_L_RecordRepository == null)
        ////            this._TA_L_RecordRepository = new Genericrepository<TA_L_Record>(_context);
        ////        return _TA_L_RecordRepository;
        ////    }
        ////}

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        ////public Genericrepository<TA_L_Payment> PaymentRepository
        ////{
        ////    get
        ////    {
        ////        if (this._TA_L_PaymentRepository == null)
        ////            this._TA_L_PaymentRepository = new Genericrepository<TA_L_Payment>(_context);
        ////        return _TA_L_PaymentRepository;
        ////    }
        ////}
       
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
