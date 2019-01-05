using R.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Controllers
{
    [Authorize]
    public class SupportController : Controller
    {
        iDescriptionModuleService _mainobj;
        iClientRecordService _clientobj;
        public SupportController(iDescriptionModuleService imainobj,iClientRecordService clientobj)
        {
            this._mainobj = imainobj;
            this._clientobj = clientobj;
        }
        //
        // GET: /Support/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            try
            {
                var currentwebsite = "Brick Kiln,";
                var results = _mainobj.GetAll();
                results = results.Where(w => (w.websitemodule != null && w.websitemodule.Contains(currentwebsite))).ToList();
                if (results != null)
                {
                   var nresults = results.AsEnumerable().Select(p => new{
                      Title =p.Title.titlename, 
                      Category=p.Category.catname, 
                      Video=p.descvideolink,
                   }).ToList();
                   return Json(new { data = nresults }, JsonRequestBehavior.AllowGet);
                }
            }
            catch { }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);

        }


        [Authorize(Roles = "Support, Admin")]
        [ActionName("ClientDetail")]
        public ActionResult Detail()
        {
            return View();
        }

        [Authorize(Roles = "Support, Admin")]
        public ActionResult DetailClient() {
            var results = _clientobj.GetAll();
            if (results != null)
            {
                results = results.AsEnumerable().Where(c => c.userrole=="Client").ToList();
                var nresults = results.AsEnumerable().Select(p => new
                {
                   recordid=p.recordid,
                    productname=p.productname,
                    productid = p.productid,
                    businessname = p.businessname,
                    clientname = p.clientname,
                    clientemail = p.clientemail,
                    contactmobile = p.contactmobile,
                    clientaddress = p.clientaddress,
                    expirydate = p.expirydate,
                    remarks=p.remarks,

                }).ToList();
                return Json(new { data = nresults }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }
    }
}
