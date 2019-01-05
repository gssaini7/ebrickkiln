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

    public class ProjectController : Controller
    {
        iDbManagerService _mainobj;
        ManagerClass Manager;
        public ProjectController(iDbManagerService imainobj)
        {
            this._mainobj = imainobj;
            this.Manager = new ManagerClass(this._mainobj);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            var results = Manager.GetAllProjects();
            if (results.Any()) 
            {
                return Json(new { data = results }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
            //var results = _mainobj.GetByType(StaticData.ManageType.Project.ToString());
            //if (results != null)
            //{
            //    var newlist = new List<ProjectEntity>();
            //    foreach (var item in results)
            //    {
            //        var newsingle = new ProjectEntity();
            //        newsingle = JsonConvert.DeserializeObject<ProjectEntity>(item.mcontent);
            //        newsingle.managerid = item.mid;
            //        newlist.Add(newsingle);
            //    }

            //    return Json(new { data = newlist }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0) return HttpNotFound();

            var result = Manager.GetProject(id);

            return PartialView("_save", result);




            //var result = _mainobj.GetById(id);

            //if (result != null)
            //{
            //    var single = new ProjectEntity();
            //    single = JsonConvert.DeserializeObject<ProjectEntity>(result.mcontent);
            //    single.managerid = result.mid;
            //    return PartialView("_save", single);
            //}

            //return PartialView("_save", result);
        }
        [HttpPost]
        public ActionResult Save(ProjectEntity input)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.managerid > 0)
                {
                    
                    var model = _mainobj.GetById(input.managerid);
                    //var oldsingle = Manager.GetProject(input.managerid);
                    var oldsingle = new ProjectEntity();
                    oldsingle = JsonConvert.DeserializeObject<ProjectEntity>(model.mcontent);
                    oldsingle.projectname = input.projectname;
                    oldsingle.projectdescription = input.projectdescription;
                    oldsingle.LastUpdatedate = DateTime.Now;

                    string json = JsonConvert.SerializeObject(oldsingle);
                    model.mcontent = json;
                    var result = _mainobj.Update(input.managerid, model);
                }
                else
                {
                    input.createdate = DateTime.Now;
                    input.createdBy = 1; ///////////////////////// Temporary data
                    input.isPublished = true;
                    input.LastUpdatedate = DateTime.Now;
                    input.publisheddate = DateTime.Now;
                    var model = new DbManagerEntity();
                    string json = JsonConvert.SerializeObject(input);
                    model.mcontent = json;
                    model.mrowtype = StaticData.ManageType.Project.ToString();

                    var result = _mainobj.Create(model);
                }
                status = true;
            }
            return new JsonResult
            {
                Data = new { status = status }
            };
        }
        
    }
}
