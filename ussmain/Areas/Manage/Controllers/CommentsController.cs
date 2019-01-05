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
    public class CommentsController : Controller
    {
        iDbManagerService _mainobj;
        iClientRecordService _clientobj;

        ManagerClass Manager;
        public CommentsController(iDbManagerService imainobj, iClientRecordService clientobj)
        {
            this._mainobj = imainobj;
            this._clientobj = clientobj;

            this.Manager = new ManagerClass(this._mainobj);
        }

        public ActionResult Index(int id)
        {
            if (id > 0)
            {
                var rowdetail = Manager.GetTask(id);
                if (rowdetail != null)
                {
                    
                        Session["taskid"] = rowdetail.managerid;
                        ViewBag.TaskName = rowdetail.taskname;
                        //return View();
                        return PartialView();
                    
                }

                
            }

            return RedirectToAction("Index", "Project");
        }
        public ActionResult GetAll()
        {
            var taskid = Convert.ToInt32(Session["taskid"]);

            var results = Manager.GetAllComments();
            if (results != null && taskid != 0)
            {
                var newlist = new List<CommentEntity>();
                foreach (var item in results)
                {
                    if (item.taskid == taskid)
                    {
                        var newsingle = Manager.GetComment(item.managerid);
                        var userassigned = _clientobj.GetById(newsingle.createdBy);
                        newsingle.CommentedBy.gsusername = userassigned.clientname;
                        newlist.Add(newsingle);
                    }
                }
                return Json(new { data = newlist }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0) return HttpNotFound();

            var result = Manager.GetComment(id);

            return PartialView("_save", result);
        }
        [HttpPost]
        public ActionResult Save(CommentEntity input)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.managerid > 0)
                {

                    var model = _mainobj.GetById(input.managerid);
                    var oldsingle = new CommentEntity();
                    oldsingle = JsonConvert.DeserializeObject<CommentEntity>(model.mcontent);
                    oldsingle.commentdetail = input.commentdetail;
                    oldsingle.LastUpdatedate = DateTime.Now;

                    string json = JsonConvert.SerializeObject(oldsingle);
                    model.mcontent = json;
                    var result = _mainobj.Update(input.managerid, model);
                    status = true;
                }
                else
                {
                    var taskid = Convert.ToInt32(Session["taskid"]);
                    if (taskid > 0)
                    {
                        input.taskid = taskid;
                        input.createdate = DateTime.Now;
                        input.createdBy = 1; ///////////////////////// Temporary data
                        input.isPublished = true;
                        input.LastUpdatedate = DateTime.Now;
                        input.publisheddate = DateTime.Now;
                        var model = new DbManagerEntity();
                        string json = JsonConvert.SerializeObject(input);
                        model.mcontent = json;
                        model.mrowtype = StaticData.ManageType.Comment.ToString();

                        var result = _mainobj.Create(model); status = true;
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
