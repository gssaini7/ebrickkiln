using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace R.BusinessEntities
{

    public class LoginModel
    {
        [Required(ErrorMessage="Enter Mobile Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid 10 digit mobile number")]
        public string MobileLogin { get; set; }

        public string Password { get; set; }
    }

    public class OTPModel
    {
        [Required(ErrorMessage="Enter OTP")]
        public string Otpvalue { get; set; }
    }



    public class updatepassword :ClientRecordModel
    {
        [Required]
        [Display(Name = "New Password")]
        public string newpassword { get; set; }
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.CompareAttribute("newpassword", ErrorMessage = "Passwords does not match.")]
        public string confirmpassword { get; set; }

    }
    public class changepassword : updatepassword
    {
        [Display(Name = "Old Password")]
        public string oldpassword { get; set; }
    }
    public class CategoryMasterModel
    {
        public int catid { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string catname { get; set; }
    }

    public partial class TitleMasterModel
    {
        public int titleid { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string titlename { get; set; }
    }

    public class ClientRecordModel
    {
        public int recordid { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string productname { get; set; }
        [Required]
        [Display(Name = "Product ID")]
        public string productid { get; set; }
        [Required]
        [Display(Name = "Business Name")]
        public string businessname { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string clientname { get; set; }
        [Required]
        [Display(Name = "Client Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string clientemail { get; set; }

        [Required]
        [Display(Name = "Client Mobile")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid 10 digit mobile number")]
        //[Remote("MobileExists", "user", HttpMethod = "POST", ErrorMessage = "User with this Mobile already exists")]
        public string contactmobile { get; set; }

        [Required]
        [Display(Name = "Client Address")]
        public string clientaddress { get; set; }
        public string formodule { get; set; }
        public string userrole { get; set; }
        public bool userblocked { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> expirydate { get; set; }

        public string upassword { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }



    }

    public class DescriptionModuleModel
    {
        public int descriptionid { get; set; }
        [Required]
        [Display(Name = "Title")]
        public Nullable<int> desctitile { get; set; }
        [Required]
        [Display(Name = "Category")]
        public Nullable<int> desccategory { get; set; }
        [Required]
        [Display(Name = "Video Link")]
        public string descvideolink { get; set; }
        public string websitemodule { get; set; }
        public string descanydescription { get; set; }

        public virtual CategoryMasterModel Category { get; set; }
        public virtual TitleMasterModel Title { get; set; }
    }

    public class BlogModuleModel
    {
       
        public long blogid { get; set; }
        public string blogtitle { get; set; }
        [AllowHtml]
        public string blogdescription { get; set; }
        public string blogimage { get; set; }
        public DateTime blogdate { get; set; }
        public DateTime blogupdate { get; set; }
        public string blogkeywords { get; set; }
        public string blogtags { get; set; }
        public string blogtagsdisplay { get; set; }
        public string blogwebsite { get; set; }
        public bool blogpublished { get; set; }

      

    }

    public class TagsModel
    {
        //public TagsModel()
        //{
        //    this.BlogTagsRel = new HashSet<BlogTagRelModel>();
        //}
        public int tagid { get; set; }
        [Required]
        [RegularExpression("^[A-Z a-z 0-9]+$", ErrorMessage = "Alpha-numeric Only")]
        public string tagname { get; set; }

        //public virtual IEnumerable<BlogTagRelModel> BlogTagsRel { get; set; }
    }
    //public partial class BlogTagRelModel
    //{
    //    public long relid { get; set; }
    //    public Nullable<long> fkblogid { get; set; }
    //    public Nullable<int> fktagid { get; set; }

    //    public virtual BlogModuleModel Blogs { get; set; }
    //    public virtual TagsModel Tags { get; set; }
    //}
   
    public class CouponEntity { 
        public int couponid { get; set; }
        [Required]
        [Display(Name = "Coupon Code")]
        public string couponcode { get; set; }
        [Display(Name = "Message for User")]

        public string cmsgforuser { get; set; }
        [Display(Name = "Admin Remarks")]

        public string cAdminRemarks { get; set; }
        [Display(Name = "Blocked Coupon")]

        public bool cblocked { get; set; }
        [Required]
        [Display(Name = "Product Amount")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        public int camount { get; set; }

    }

    public class ProjectDetailEntity
    {
        public int amtdtid { get; set; }
        public string projectdetail { get; set; }
        public string prowtype { get; set; }
    }

    public class ProjectDetailEntityModel
    {
        public int amtdtid { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public string projectname { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string projecttype { get; set; }
        [Required]
        [Display(Name = " Cost")]
        public int projectcost { get; set; }
        [Required]
        [Display(Name = "Amount Advance")]
        public int projectadvance { get; set; }
        [Required]
        [Display(Name = "Amount Pending")]
        public int projectpending { get; set; }
        [Required]
        [Display(Name = "Deliverables")]
        public string projectdeliverables { get; set; }
        [Required]
        [Display(Name = "Deliverables Pending")]
        public string projectdeliverablespending { get; set; }

        [Display(Name = "Deliverables Completed")]
        public string projectdeliverablescompleted { get; set; }
    }

    public class ClientComments
    {
        public long cmntid { get; set; }
        [Required]

        [Display(Name = "Type")]
        public string cmntype { get; set; }
        [Required]

        [Display(Name = "Detail")]
        public string cmntcontent { get; set; }
        [Required]

        [Display(Name = "Date")]
        public Nullable<System.DateTime> cmntdate { get; set; }
        public Nullable<System.DateTime> cmntcreatedate { get; set; }
        public Nullable<long> userfkid { get; set; }
    }
   
}
