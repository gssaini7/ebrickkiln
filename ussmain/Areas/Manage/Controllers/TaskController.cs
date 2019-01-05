using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ussmain.Areas.Manage.Models;

namespace ussmain.Areas.Manage.Controllers
{
    [Authorize(Roles = "Local")]

    public class TaskController : Controller
    {
        iDbManagerService _mainobj;
        iClientRecordService _clientobj;
        ManagerClass Manager;

        public TaskController(iDbManagerService imainobj, iClientRecordService clientobj)
        {
            this._mainobj = imainobj;
            this._clientobj = clientobj;
            this.Manager = new ManagerClass(this._mainobj);

        }

        public ActionResult Index(int id)//project id
        {
            if (id > 0)
            {
                var rowdetail = Manager.GetProject(id);
                if (rowdetail != null) 
                {
                    Session["projectid"] = rowdetail.managerid;
                    ViewBag.ProjectName = rowdetail.projectname;
                    return View();
                }

                //var rowdetail = _mainobj.GetById(id);
                //if (rowdetail != null)
                //{
                //    Session["projectid"] = rowdetail.mid;
                //    var projectdetail = new ProjectEntity();
                //    projectdetail = JsonConvert.DeserializeObject<ProjectEntity>(rowdetail.mcontent);
                //    ViewBag.ProjectName = projectdetail.projectname;
                //    return View();
                //}
            }
         
            return RedirectToAction("Index", "Project");
        }
        public ActionResult GetAll()
        {
            var projectid = Convert.ToInt32(Session["projectid"]);

            var results = Manager.GetAllTasks();
            if (results != null && projectid != 0)
            {
                var newlist = new List<TaskEntity>();
                foreach (var item in results)
                {
                    if (item.projectid == projectid) 
                    {
                        var newsingle = Manager.GetTask(item.managerid);
                        var userassigned = _clientobj.GetById(newsingle.AssignedTo);
                        newsingle.AssignedUser.gsusername = userassigned.clientname;
                        newlist.Add(newsingle);
                    }
                }
                return Json(new { data = newlist }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
            //var results = _mainobj.GetByType(StaticData.ManageType.Task.ToString());
            //if (results != null && projectid!=0)
            //{
            //    var newlist = new List<TaskEntity>();
            //    foreach (var item in results)
            //    {
            //        var newsingle = new TaskEntity();
            //        newsingle = JsonConvert.DeserializeObject<TaskEntity>(item.mcontent);
            //        newsingle.managerid = item.mid;


            //        var userassigned = _clientobj.GetById(newsingle.AssignedTo);
            //        newsingle.AssignedUser.gsusername = userassigned.clientname;

            //        if(newsingle.projectid==projectid)
            //            newlist.Add(newsingle);
            //    }

            //    return Json(new { data = newlist }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0) return HttpNotFound();

            var result = Manager.GetTask(id);

            return PartialView("_save", result);

            //var result = _mainobj.GetById(id);

            //if (result != null)
            //{
            //    var single = new TaskEntity();
            //    single = JsonConvert.DeserializeObject<TaskEntity>(result.mcontent);
            //    single.managerid = result.mid;
            //    return PartialView("_save", single);
            //}

            //return PartialView("_save", result);
        }
        [HttpPost]
        public ActionResult Save(TaskEntity input)
        {
            
           
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.managerid > 0)
                {
                    var model = _mainobj.GetById(input.managerid);

                    var oldsingle = new TaskEntity();
                    oldsingle = JsonConvert.DeserializeObject<TaskEntity>(model.mcontent);
                    oldsingle.AmtPaid = input.AmtPaid;
                    oldsingle.AssignedTo = input.AssignedTo;
                    oldsingle.Cost = input.Cost;
                    oldsingle.LastUpdatedate = DateTime.Now;
                    oldsingle.taskdescription = input.taskdescription;
                    oldsingle.taskname = input.taskname;
                    oldsingle.CurrentStatus = input.CurrentStatus;

                    string json = JsonConvert.SerializeObject(oldsingle);
                    model.mcontent = json;
                    var result = _mainobj.Update(input.managerid, model);
                    status = true;
                }
                else
                {
                    var projectid = Convert.ToInt32(Session["projectid"]);
                    if (projectid > 0)
                    {
                        input.projectid = projectid;
                        input.createdate = DateTime.Now;
                        input.createdBy = 1; ///////////////////////// Temporary data
                        input.isPublished = true;
                        input.LastUpdatedate = DateTime.Now;
                        input.publisheddate = DateTime.Now;
                        var model = new DbManagerEntity();

                        string json = JsonConvert.SerializeObject(input);
                        model.mcontent = json;
                        model.mrowtype = StaticData.ManageType.Task.ToString();

                        var result = _mainobj.Create(model);
                        status = true;
                    }
                }
               
            }
            return new JsonResult
            {
                Data = new { status = status }
            };
        }

    }
}
