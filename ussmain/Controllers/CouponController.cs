using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ebrickkiln.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CouponController : Controller
    {
       iCouponService _mainobj; public CouponController(iCouponService imainobj) { this._mainobj = imainobj; } 
        public ActionResult Index() { return View(); }
        public ActionResult GetAll()
        {
            var results = _mainobj.GetAll(); if (results != null)
            {
                return Json(new { data = results }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Save(CouponEntity input)
        {
            bool status = false; 
            if (ModelState.IsValid)
            {
                if (input.couponid > 0)
                {
                    input.couponcode = input.couponcode.ToLower();
                    var result = _mainobj.Update(input.couponid, input);
                }
                else
                {
                    input.couponcode = input.couponcode.ToLower();
                    var result = _mainobj.Create(input);
                } 
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
