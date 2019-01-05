using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace R.BusinessEntities.Manager
{
    public class EntityCommon 
    {
        public long managerid { get; set; }
        public DateTime createdate { get; set; }
        public DateTime LastUpdatedate { get; set; }
        public DateTime publisheddate { get; set; }
        public bool isPublished { get; set; }
        public int createdBy { get; set; }
        public string managerremarks { get; set; }
    }

    public class TaskEntity : EntityCommon
    {
        public TaskEntity() 
        {
            this.AssignedUser = new UserEntity();
        }

        public long projectid { get; set; }
        public string taskname { get; set; }
        public string taskdescription { get; set; }
        public long AssignedTo { get; set; }
        public int Cost { get; set; }
        public int AmtPaid { get; set; }
        public string CurrentStatus { get; set; }
        public UserEntity AssignedUser { get; set; }
    }

    public class ProjectEntity : EntityCommon
    {
        public string projectname { get; set; }
        public string projectdescription { get; set; }
    }

    public class CommentEntity : EntityCommon
    {
        public CommentEntity() 
        {
            this.CommentedBy = new UserEntity();
        }

        public string commentdetail { get; set; }
        public long taskid { get; set; }

        public UserEntity CommentedBy { get; set; }

    }

    public class UserEntity : EntityCommon
    {
        public string gsusername { get; set; }
        public string guseremail { get; set; }
        public string gusermobile { get; set; }
        public string guserdetails { get; set; }

    }

    public partial class DbManagerEntity
    {
        public long mid { get; set; }
        public string mguid { get; set; }
        public string mcontent { get; set; }
        public string mrowtype { get; set; }
    }


   
}
