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

    public class DashboardController : Controller
    {
        iDbManagerService _mainobj;
        iClientRecordService _clientobj;
        ManagerClass Manager;
        public DashboardController(iDbManagerService imainobj, iClientRecordService clientobj)
        {
            this._mainobj = imainobj;
            this._clientobj = clientobj;
            this.Manager = new ManagerClass(this._mainobj);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllProjects()
        {
            var newlist = Manager.GetAllProjects();
            return PartialView("_AllProjects", newlist);
        }

        public ActionResult Tasks(int id)
        {
            var project = Manager.GetProject(id);
            if (project != null)
            {
                ViewBag.ProjectName = project.projectname;
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var results = Manager.GetAllTasks();
            if (results != null && id != 0)
            {
                var newlist = new List<TaskEntity>();
                foreach (var item in results)
                {
                    if (item.projectid == id)
                    {
                        var newsingle = Manager.GetTask(item.managerid);

                        var userassigned = _clientobj.GetById(newsingle.AssignedTo);
                        newsingle.AssignedUser.gsusername = userassigned.clientname;

                        newlist.Add(newsingle);
                    }
                }
                return View(newlist);
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
