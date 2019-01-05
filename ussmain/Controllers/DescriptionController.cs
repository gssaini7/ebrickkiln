using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DescriptionController : Controller
    {
        iDescriptionModuleService _mainobj;
        public DescriptionController(iDescriptionModuleService imainobj)
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
                results = results.OrderByDescending(o => o.descriptionid).ToList();
                return Json(new { data = results}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0)
                return HttpNotFound();

            var result = _mainobj.GetById(id);
            
            return PartialView("_save", result);
           
        }

        [HttpPost]
        public ActionResult Save(DescriptionModuleModel input, string[] forwebsite)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                input.websitemodule = forwebsite != null ? StringArrayToString(forwebsite) : null;

                if (input.descriptionid > 0)
                {
                    var olddata = _mainobj.GetById(input.descriptionid);
                    var result = _mainobj.Update(input.descriptionid, input);
                }
                else
                {
                    var result = _mainobj.Create(input);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        private string StringArrayToString(string[] strArray) 
        {
            var result = string.Empty;
            foreach (var item in strArray)
            {
                result += item + ", ";
            }
            return result;
        }

    }
}
