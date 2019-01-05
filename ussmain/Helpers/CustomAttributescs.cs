using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Helpers
{
    class MyCustomFilter : FilterAttribute, IAuthorizationFilter
    {
        string _role;
        public MyCustomFilter(string role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            //if (filterContext.HttpContext.User == null) 
            //{
            //    filterContext.Result = new RedirectResult("/");
            //}
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new RedirectResult("/");
            //}
            if (!filterContext.HttpContext.User.IsInRole(_role))
            {
                filterContext.Result = new RedirectResult("/");
            }
          
        }
    }
}