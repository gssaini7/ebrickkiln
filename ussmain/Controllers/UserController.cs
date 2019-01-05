using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ussmain;

namespace ebrickkiln.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        iClientRecordService _mainobj;
        public UserController(iClientRecordService imainobj)
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
                results = results.AsEnumerable().Where(c =>(c.userrole!=null && !c.userrole.Contains("Local"))).ToList(); // to remove user from list, for hidden module
                var nresults = results.AsEnumerable().Select(p => new
                {
                    productname = p.productname,
                    productid = p.productid,
                    businessname = p.businessname,
                    clientname = p.clientname,
                    clientemail = p.clientemail,
                    contactmobile = p.contactmobile,
                    clientaddress = p.clientaddress,
                    expirydate = p.expirydate,
                    formodule = p.formodule,
                    recordid = p.recordid,
                    userblocked = p.userblocked,
                   remarks=p.remarks,
                }).ToList();
               
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
        public ActionResult Save(ClientRecordModel input, string[] forwebsite)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.recordid > 0)
                {
                    input.formodule = forwebsite != null ? StringArrayToString(forwebsite) : null;
                    var olddata = _mainobj.GetById(input.recordid);
                    olddata.businessname = input.businessname;
                    olddata.clientaddress = input.clientaddress;
                    olddata.clientemail = input.clientemail.Trim();
                    olddata.clientname = input.clientname;
                    olddata.contactmobile = input.contactmobile.Trim();
                    olddata.formodule = input.formodule;
                    olddata.productid = input.productid;
                    olddata.productname = input.productname;
                    olddata.userblocked = input.userblocked;
                    olddata.expirydate = input.expirydate;
                    olddata.remarks = input.remarks;

                    //input.formodule = forwebsite != null ? StringArrayToString(forwebsite) : null;
                    var result = _mainobj.Update(input.recordid, olddata);
                    status = true;
                    return new JsonResult { Data = new { status = status, message = "User record Updated" } };
                }
                else
                {
                    var clientrecord = _mainobj.GetByMobile(input.contactmobile);
                    if (clientrecord != null)
                    {
                        return new JsonResult { Data = new { status = status, error = "Mobile number already existed." } };
                    }
                    
                        input.userrole = "Client";
                        input.formodule = forwebsite != null ? StringArrayToString(forwebsite) : null;
                        var result = _mainobj.Create(input);
                        status = true;
                        return new JsonResult { Data = new { status = status, message = "User record inserted" } };
                }
                
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

        [HttpGet]
        public ActionResult Lock(int id)
        {
            if (id <= 0)
                return HttpNotFound();

            var result = _mainobj.GetById(id);
            if (result != null)
                return PartialView("_Lock", result);
            else
                return HttpNotFound();

        }

        [HttpPost]
        [ActionName("Lock")]
        public ActionResult LockConfirm(int id)
        {
            bool status = false;
            var records = _mainobj.GetById(id);
            if (records != null)
            {
                if (records.userblocked)
                    records.userblocked = false;
                else
                    records.userblocked = true;

                var result = _mainobj.Update(id, records);
                if (result)
                    status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            if (id <= 0)
                return HttpNotFound();
            var result = _mainobj.GetById(id);

            if (result != null)
            {
                var newpasswordmodel = new updatepassword()
                {
                    clientname = result.clientname,
                    clientemail = result.clientemail,
                    recordid = result.recordid,
                };
                return PartialView("_ChangePassword", newpasswordmodel);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        public ActionResult ChangePassword(updatepassword input)
        {
            bool status = false;
            var message = "Password changed failed";

            ModelState.Remove("productname");
            ModelState.Remove("productid");
            ModelState.Remove("businessname");
            ModelState.Remove("clientname");
            ModelState.Remove("clientemail");
            ModelState.Remove("contactmobile");
            ModelState.Remove("clientaddress");
            ModelState.Remove("expirydate");

            if (ModelState.IsValid)
            {
                if (input.recordid > 0)
                {
                    var olddata = _mainobj.GetById(input.recordid);
                    olddata.upassword = StaticData.GetSHA512(input.newpassword);
                    var result = _mainobj.Update(input.recordid, olddata);

                }
               
                status = true;

                message = "Password Succesfully changed";
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }


        //public ActionResult MobileExists(string contactmobile)
        //{
        //    if (contactmobile != string.Empty)
        //    {
        //        var clientrecord = _mainobj.GetByMobile(contactmobile);
        //        if (clientrecord != null)
        //        {
        //            return Json(false, JsonRequestBehavior.AllowGet);
        //        }
                              
        //    }
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

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
