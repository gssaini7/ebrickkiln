using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ussmain.Helpers;

namespace ussmain.Controllers
{
    [Authorize(Roles = "Support, Admin")]
    //[MyCustomFilter("Admin")]
    public class UserCommentsController : Controller
    {
        iClientCommentService _mainobj;
        public UserCommentsController(iClientCommentService imainobj)
        {
            this._mainobj = imainobj;
        }


        public ActionResult Index(string id)
        {
            if (id != null)
            {
                Session["clientuserid"] = id;
                return View();
            }
            return RedirectToAction("Index", "User");
        }


        public ActionResult GetAll()
        {
            var id = Convert.ToInt32(Session["clientuserid"]);
            
            var results = _mainobj.GetAllByOther(id);
            if (results != null)
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
        public ActionResult Save(ClientComments input)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.cmntid > 0)
                {
                    var result = _mainobj.Update(input.cmntid, input);
                }
                else
                {
                    input.cmntcreatedate = DateTime.Now;
                    input.userfkid = Convert.ToInt32(Session["clientuserid"]);
                    var result = _mainobj.Create(input);
                }
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        bool status = false;
        //        if (id > 0)
        //        {
        //            var result = _mainobj.Delete(id);
        //            status = true;
        //            return new JsonResult { Data = new { status = status } };
        //        }
        //        return new JsonResult { Data = new { status = status, message = "Record not deleted." } };
        //    }
        //    catch
        //    {
        //        return new JsonResult { Data = new { status = false, message = "Record not deleted." } };

        //    }

        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Session["deleteid"] = id;
            return PartialView("_delete");
        }

        [HttpPost]
        public ActionResult DeleteConfirm()
        {
            try
            {
                int id = Convert.ToInt32(Session["deleteid"]);
                bool status = false;
                if (id > 0)
                {
                    var result = _mainobj.Delete(id);
                    status = true;
                    return new JsonResult { Data = new { status = status } };
                }
                return new JsonResult { Data = new { status = status, message = "Record not deleted." } };
            }
            catch
            {
                return new JsonResult { Data = new { status = false, message = "Record not deleted." } };

            }

        }
    }
}
