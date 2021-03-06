﻿using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ebrickkiln.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TitleController : Controller
    {
        iTitleMasterService _mainobj;
        public TitleController(iTitleMasterService imainobj)
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
                results = results.OrderBy(o => o.titlename).ToList();
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
        public ActionResult Save(TitleMasterModel input)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.titleid > 0)
                {
                    var result = _mainobj.Update(input.titleid, input);
                }
                else
                {
                    var result = _mainobj.Create(input);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    if (id <= 0)
        //        return HttpNotFound();

        //    var result = _mainobj.GetById(id);
        //    if (result != null)
        //        return View("_Delete", result);
        //    else
        //        return HttpNotFound();

        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    bool status = false;
        //    var records = _mainobj.GetById(id);
        //    if (records != null)
        //    {
        //        var result = _mainobj.Update(id, records);
        //        if (result)
        //            status = true;
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}
    }
}
