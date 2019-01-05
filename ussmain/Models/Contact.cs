using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ussmain.Models
{
    public class PaymentContact {
        [Required(ErrorMessage = "Enter name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only")]
        public string pcName { get; set; }

        [Required(ErrorMessage = "Enter email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string pcEmail { get; set; }

        [Required(ErrorMessage = "Enter mobile")]
        [Display(Name = "Mobile")]
        public string pcMobile { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [Display(Name = "City")]
        public string pcCity { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string pcAddress { get; set; }

        [Required(ErrorMessage = "Enter Zip")]
        [Display(Name = "Zip")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Enter valid 6 digit pin code")]
        public string pcZip { get; set; }

        [Display(Name = "Coupon")]
        public string pcCoupon { get; set; }

        [Required(ErrorMessage = "Enter Amount")]
        [Display(Name = "Amount")]
        [RegularExpression(@"^[0-9]{3,8}$", ErrorMessage = "Enter valid amount")]
        public string pcAmount { get; set; }
    }


    public class Contact
    {
        [Required(ErrorMessage="Enter name")]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only")]
        public string Name { get; set; }

        [Required(ErrorMessage="Enter email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter mobile")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage="Enter message")]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }

    public class QuoteModel {
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Contact Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid 10 digit mobile number")]
        [Display(Name = "Contact Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Select package type")]
        [Display(Name = "Package Type")]
        public string package_type { get; set; }

        [Required(ErrorMessage = "Enter BKO or Business Name")]
        [Display(Name = "BKO or Business Name")]
        public string Org_Name { get; set; }

        [Required(ErrorMessage = "Select Brick Kiln or Business Type")]
        [Display(Name = "Brick Kiln or Business Type")]
        public string busiess_type { get; set; }

        [Required(ErrorMessage = "Enter City or Village")]
        [Display(Name = "City or Village")]
        public string City_village { get; set; }
        
        public string District { get; set; }
        
        public string State { get; set; }
        
        public string Country { get; set; }
        
        [Display(Name = "See the Video Demo First")]
        public string seen_video { get; set; }
        
        [Display(Name = "Interested For")]
        public string Interested_for { get; set; }
    }

   
}