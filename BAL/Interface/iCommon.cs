using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.BusinessEntities;
using System.Threading.Tasks;
using R.BusinessEntities.Manager;

namespace R.BAL
{
    public interface iCommon<T> where T : class
    {
        T GetById(long tid);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByOther(long otherid);
        long Create(T tentity);
        bool Update(long tid, T tentity);
        bool Delete(long tid);

    }

   

    public interface iCategoryMasterService : iCommon<CategoryMasterModel> { }
    public interface iTitleMasterService : iCommon<TitleMasterModel> { }
    public interface iClientRecordService : iCommon<ClientRecordModel> {
        ClientRecordModel GetByMobile(string mobileno);
        ClientRecordModel GetByMobilePassword(string mobileno, string usrpassword);
        

    }
    public interface iDescriptionModuleService : iCommon<DescriptionModuleModel> { }
    public interface iBlogModuleService : iCommon<BlogModuleModel> {
        BlogModuleModel GetByBlogNameKeyword(string keywords);
        IEnumerable<BlogModuleModel> GetAllByOther(string[] otherid);

    }
    public interface iTagsModuleService : iCommon<TagsModel> {
        IEnumerable<TagsModel> GetAllByOther(string[] otherid);
        TagsModel GetByName(string tid);

    }


    public interface iCouponService : iCommon<CouponEntity> {
        CouponEntity GetByCouponCode(string couponcode);
    
    }


    public interface iDbManagerService : iCommon<DbManagerEntity> {
        //DbManagerEntity GetById(string tid);
        IEnumerable<DbManagerEntity> GetByType(string type);
    }

    public interface iProjectDetailService : iCommon<ProjectDetailEntity> { }
    public interface iClientCommentService : iCommon<ClientComments> { }




}
