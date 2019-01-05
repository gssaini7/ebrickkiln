using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Areas.Manage.Controllers
{
    [Authorize(Roles = "Local")]

    public class UsersController : Controller
    {
        iClientRecordService _mainobj;
        public UsersController(iClientRecordService imainobj)
        {
            this._mainobj = imainobj;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var results = _mainobj.GetAll();
            if (results != null)
            {
                results = results.AsEnumerable().Where(c => (c.userrole != null && c.userrole.Contains("Admin"))).ToList(); 
                var nresults = results.AsEnumerable().Select(p => new
                {
                    //productname = p.productname,
                    //productid = p.productid,
                    //businessname = p.businessname,
                    clientname = p.clientname,
                    clientemail = p.clientemail,
                    contactmobile = p.contactmobile,
                    //clientaddress = p.clientaddress,
                    //expirydate = p.expirydate,
                    //formodule = p.formodule,
                    //recordid = p.recordid,
                    userblocked = p.userblocked,
                    //remarks = p.remarks,
                }).ToList();

                return Json(new { data = results }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllForDropDown()
        {
            var results = _mainobj.GetAll();
            if (results != null)
            {
                results = results.AsEnumerable().Where(c => (c.userrole != null && c.userrole.Contains("Admin"))).ToList();
                var nresults = results.AsEnumerable().Select(p => new
                {
                    ddid=p.recordid,
                    ddname=p.clientname,
                }).ToList();

                return Json(new { data = nresults }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);

        }

       
    }
}
