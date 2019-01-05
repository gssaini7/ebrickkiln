using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Areas.Manage.Models
{
   

    public class ManagerClass 
    {
        iDbManagerService _mainobj;

        public ManagerClass(iDbManagerService imainobj)
        {
            this._mainobj = imainobj;
        }

      

        public IEnumerable<ProjectEntity> GetAllProjects()
        {
            var results = _mainobj.GetByType(StaticData.ManageType.Project.ToString());
            if (results != null)
            {
                var newlist = new List<ProjectEntity>();
                foreach (var item in results)
                {
                    var newsingle = new ProjectEntity();
                    newsingle = JsonConvert.DeserializeObject<ProjectEntity>(item.mcontent);
                    newsingle.managerid = item.mid;
                    newlist.Add(newsingle);
                }
                return newlist;
            }
            return null;
        }

        public ProjectEntity GetProject(long id)
        {
            if (id < 0) return null;
            var result = _mainobj.GetById(id);
            if (result != null)
            {
                if (result.mrowtype == StaticData.ManageType.Project.ToString())
                {
                    var single = new ProjectEntity();
                    single = JsonConvert.DeserializeObject<ProjectEntity>(result.mcontent);
                    single.managerid = result.mid;
                    return single;
                }
            }
            return null;
        }

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            var results = _mainobj.GetByType(StaticData.ManageType.Task.ToString());
            if (results != null)
            {
                var newlist = new List<TaskEntity>();
                foreach (var item in results)
                {
                    var newsingle = new TaskEntity();
                    newsingle = JsonConvert.DeserializeObject<TaskEntity>(item.mcontent);
                    newsingle.managerid = item.mid;
                    newlist.Add(newsingle);
                }
                return newlist;
            }
            return null;
        }

       

        public TaskEntity GetTask(long id)
        {
            if (id < 0) return null;
            var result = _mainobj.GetById(id);
            if (result != null)
            {
                if (result.mrowtype == StaticData.ManageType.Task.ToString())
                {
                    var single = new TaskEntity();
                    single = JsonConvert.DeserializeObject<TaskEntity>(result.mcontent);
                    single.managerid = result.mid;
                    return single;
                }
            }
            return null;
        }

        public IEnumerable<CommentEntity> GetAllComments()
        {
            var results = _mainobj.GetByType(StaticData.ManageType.Comment.ToString());
            if (results != null)
            {
                var newlist = new List<CommentEntity>();
                foreach (var item in results)
                {
                    var newsingle = new CommentEntity();
                    newsingle = JsonConvert.DeserializeObject<CommentEntity>(item.mcontent);
                    newsingle.managerid = item.mid;
                    newlist.Add(newsingle);
                }
                return newlist;
            }
            return null;
        }



        public CommentEntity GetComment(long id)
        {
            if (id < 0) return null;
            var result = _mainobj.GetById(id);
            if (result != null)
            {
                if (result.mrowtype == StaticData.ManageType.Comment.ToString())
                {
                    var single = new CommentEntity();
                    single = JsonConvert.DeserializeObject<CommentEntity>(result.mcontent);
                    single.managerid = result.mid;
                    return single;
                }
            }
            return null;
        }
    }
}