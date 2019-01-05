using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ebrickkiln.Controllers
{

    //[Authorize(Roles = "Local")]
    public class ProjectDetailController : Controller
    {
        iProjectDetailService _mainobj;
        public ProjectDetailController(iProjectDetailService imainobj)
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
                var nresultslist = new List<ProjectDetailEntityModel>();
                foreach (var item in results)
                {
                   var nresults  = GSJsonDeserialze(item.projectdetail);
                    nresults.amtdtid = item.amtdtid;
                    nresultslist.Add(nresults);
                }

                return Json(new { data = nresultslist }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0) return HttpNotFound();
            var result = _mainobj.GetById(id);

            if (result != null)
            {
                var nresults = GSJsonDeserialze(result.projectdetail);
                nresults.amtdtid = result.amtdtid;
                return PartialView("_save", nresults);

            }

            return PartialView("_save", result);
        }
        [HttpPost]
        public ActionResult Save(ProjectDetailEntityModel input)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (input.amtdtid > 0)
                {
                    var pde = new ProjectDetailEntity() { 
                        amtdtid=input.amtdtid,
                        projectdetail = GSJsonSerialze(input),
                    };

                    var result = _mainobj.Update(pde.amtdtid, pde);
                }
                else
                {
                    var pde = new ProjectDetailEntity()
                    {
                        projectdetail = GSJsonSerialze(input),
                    };
                    var result = _mainobj.Create(pde);
                }
                status = true;
            }
            return new JsonResult
            {
                Data = new { status = status }
            };
        }


        //private ProjectDetailEntity GSJsonDeserialze(IEnumerable<ProjectDetailEntity> results)
        //{
        //    ProjectDetailEntity newdetails = new ProjectDetailEntity();
        //    foreach (var item in results)
        //    {
        //        newdetails = JsonConvert.DeserializeObject<ProjectDetailEntity>(item.projectdetail);
        //    }
        //    return newdetails;
        //}

        private ProjectDetailEntityModel GSJsonDeserialze(string item)
        {
            ProjectDetailEntityModel newdetails = JsonConvert.DeserializeObject<ProjectDetailEntityModel>(item);
            return newdetails;
        }
        private string GSJsonSerialze(ProjectDetailEntityModel quizdetails)
        {
            string json = JsonConvert.SerializeObject(quizdetails);
            return json;
        }
    }
}
