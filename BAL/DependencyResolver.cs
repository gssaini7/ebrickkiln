using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.Resolver;
using System.ComponentModel.Composition;

namespace R.BAL
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<iCategoryMasterService, CategoryMasterServiceG>();
            registerComponent.RegisterType<iTitleMasterService, TitleMasterServiceG>();
            registerComponent.RegisterType<iClientRecordService, ClientRecordServiceG>();
            registerComponent.RegisterType<iDescriptionModuleService, DescriptionModuleServiceG>();
            registerComponent.RegisterType<iCouponService, CouponService>();
            registerComponent.RegisterType<iProjectDetailService, ProjectDetailService>();
            registerComponent.RegisterType<iDbManagerService, DbManagerService>();
            registerComponent.RegisterType<iBlogModuleService, BlogsModuleServiceG>();
            registerComponent.RegisterType<iTagsModuleService, TagsModuleServiceG>();
            registerComponent.RegisterType<iClientCommentService, ClientCommentServiceG>();

        }
    }
}
