using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoMapper;
using R.BusinessEntities;
using DAL;
using R.BusinessEntities.Manager;

namespace R.BAL
{
    //public static class GsMapperConfigGeneric<fromT, toT>
    //{
    //    public static IEnumerable<toT> MapperList(IEnumerable<fromT> listdbentity)
    //    {
    //        var result = new List<toT>();
    //        foreach (var item in listdbentity)
    //        {
    //            result.Add(Mapper(item));
    //        }
    //        return result;
    //    }

    //    public static toT Mapper(fromT dbentity)
    //    {
    //        var result = new toT();
    //        result.authorid = dbentity.authorid;
    //        result.Authors = dbentity.Authors;
    //        result.FontName = dbentity.FontName;
    //        return result;
    //    }
    //}

    public static class GsMapperConfig
    {

        public static IEnumerable<CategoryMasterModel> CategoryMasterMapperList(IEnumerable<ta_ussbk_categoryMaster> listdbentity)
        {
            var result = new List<CategoryMasterModel>();
            foreach (var item in listdbentity)
            {
                result.Add(CategoryMasterMapper(item));
            }
            return result;
        }

        public static CategoryMasterModel CategoryMasterMapper(ta_ussbk_categoryMaster tentity)
        {
            if (tentity != null)
            {
                var resultl = new CategoryMasterModel
                {
                    catid = tentity.catid,
                    catname = tentity.catname,
                };
                return resultl;
            }
            return null;

        }

        public static IEnumerable<TitleMasterModel> TitleMasterMapperList(IEnumerable<ta_ussbk_TitleMaster> listdbentity)
        {
            var result = new List<TitleMasterModel>();
            foreach (var item in listdbentity)
            {
                result.Add(TitleMasterMapper(item));
            }
            return result;
        }

        public static TitleMasterModel TitleMasterMapper(ta_ussbk_TitleMaster tentity)
        {
            if (tentity != null)
            {
                var resultl = new TitleMasterModel
                {
                    titleid = tentity.titleid,
                    titlename = tentity.titlename,
                };
                return resultl;
            }
            return null;
        }

        public static IEnumerable<ClientRecordModel> ClientRecordMapperList(IEnumerable<ta_ussbk_clientrecord> listdbentity)
        {
            var result = new List<ClientRecordModel>();
            foreach (var item in listdbentity)
            {
                result.Add(ClientRecordMapper(item));
            }
            return result;
        }

        public static ClientRecordModel ClientRecordMapper(ta_ussbk_clientrecord tentity)
        {
            var resultl = new ClientRecordModel
            {
                businessname = tentity.businessname,
                clientaddress = tentity.clientaddress,
                clientemail = tentity.clientemail,
                clientname = tentity.clientname,
                contactmobile = tentity.contactmobile,
                formodule = tentity.formodule,
                productid = tentity.productid,
                productname = tentity.productname,
                recordid = tentity.recordid,
                userrole = tentity.userrole,
                userblocked = tentity.userblocked.Value,
                expirydate = tentity.expirydate,
                upassword = tentity.upassword,
                remarks=tentity.remarks,
            };
            return resultl;

        }
        public static IEnumerable<DescriptionModuleModel> DescriptionModuleMapperList(IEnumerable<ta_ussbk_Description> listdbentity)
        {
            var result = new List<DescriptionModuleModel>();
            foreach (var item in listdbentity)
            {
                result.Add(DescriptionModuleMapper(item));
            }
            return result;
        }

        public static DescriptionModuleModel DescriptionModuleMapper(ta_ussbk_Description tentity)
        {
            var resultl = new DescriptionModuleModel
            {
                descanydescription = tentity.descanydescription,
                desccategory = tentity.desccategory,
                descriptionid = tentity.descriptionid,
                desctitile = tentity.desctitile,
                descvideolink = tentity.descvideolink,
                websitemodule = tentity.websitemodule,
                Category = CategoryMasterMapper(tentity.ta_ussbk_categoryMaster),
                Title = TitleMasterMapper(tentity.ta_ussbk_TitleMaster),

            };
            return resultl;

        }

        public static IEnumerable<CouponEntity> CouponMapperList(IEnumerable<TA_ussbk_Coupons> listdbentity)
        {
            var result = new List<CouponEntity>();
            foreach (var item in listdbentity)
            {
                result.Add(CouponMapper(item));
            } return result;
        }
        public static CouponEntity CouponMapper(TA_ussbk_Coupons tentity)
        {
            var resultl = new CouponEntity
            {
                couponid = tentity.couponid,
                couponcode = tentity.couponcode,
                cmsgforuser = tentity.cmsgforuser,
                cAdminRemarks = tentity.cAdminRemarks,
                cblocked = tentity.cblocked.HasValue ? tentity.cblocked.Value : false,
                camount = tentity.camount.Value,
            };
            return resultl;
        }


        public static IEnumerable<ProjectDetailEntity> ProjectDetailMapperList(IEnumerable<ta_ussbk_ProjectDetail> listdbentity)
        {
            var result = new List<ProjectDetailEntity>();
            foreach (var item in listdbentity)
            {
                result.Add(ProjectDetailMapper(item));
            }
            return result;
        }
        public static ProjectDetailEntity ProjectDetailMapper(ta_ussbk_ProjectDetail tentity)
        {
            var resultl = new ProjectDetailEntity
            {
                amtdtid = tentity.amtdtid,
                projectdetail = tentity.projectdetail,
            };
            return resultl;
        }


        public static IEnumerable<DbManagerEntity> DbManagerEntityMapperList(IEnumerable<Ta_Manager> listdbentity)
        {
            var result = new List<DbManagerEntity>();
            foreach (var item in listdbentity)
            {
                result.Add(DbManagerEntityMapper(item));
            }
            return result;
        }
        public static DbManagerEntity DbManagerEntityMapper(Ta_Manager tentity)
        {
            var resultl = new DbManagerEntity
            {
                mid = tentity.mid,
                mguid = tentity.mguid,
                mcontent = tentity.mcontent,
                mrowtype = tentity.mrowtype,

            };
            return resultl;
        }

        public static IEnumerable<TagsModel> TagsModelEntityMapperList(IEnumerable<ta_blog_tags> listdbentity)
        {
            var result = new List<TagsModel>();
            foreach (var item in listdbentity)
            {
                result.Add(TagsModelEntityMapper(item));
            }
            return result;
        }
        public static TagsModel TagsModelEntityMapper(ta_blog_tags tentity)
        {
            var resultl = new TagsModel
            {
                tagid=tentity.tagid,
                tagname=tentity.tagname,
                //BlogTagsRel = BlogTagRelMapperList(tentity.ta_blog_tag_rel),
            };
            return resultl;
        }

        public static IEnumerable<BlogModuleModel> BlogEntityMapperList(IEnumerable<ta_blog> listdbentity)
        {
            var result = new List<BlogModuleModel>();
            foreach (var item in listdbentity)
            {
                result.Add(BlogEntityMapper(item));
            }
            return result;
        }
        public static BlogModuleModel BlogEntityMapper(ta_blog tentity)
        {
            var resultl = new BlogModuleModel
            {
               blogdate=tentity.blogdate,
               blogdescription=tentity.blogdescription,
               blogid=tentity.blogid,
               blogimage=tentity.blogimage,
               blogkeywords=tentity.blogkeywords,
               blogtags=tentity.blogtags,
               blogtitle=tentity.blogtitle,
               blogupdate=tentity.blogupdate,
               blogwebsite=tentity.blogwebsite,
               blogpublished=tentity.blogpublished,
            };
            return resultl;
        }


        public static IEnumerable<ClientComments> ClientCommentMapperList(IEnumerable<ta_ussbk_client_comment> listdbentity)
        {
            var result = new List<ClientComments>();
            foreach (var item in listdbentity)
            {
                result.Add(ClientCommentMapper(item));
            }
            return result;
        }
        public static ClientComments ClientCommentMapper(ta_ussbk_client_comment tentity)
        {
            var resultl = new ClientComments
            {
                cmntcontent = tentity.cmntcontent,
                cmntcreatedate = tentity.cmntcreatedate,
                cmntdate = tentity.cmntdate,
                cmntid = tentity.cmntid,
                cmntype = tentity.cmntype,
                userfkid = tentity.userfkid,
            };
            return resultl;
        }
       
    }
}
