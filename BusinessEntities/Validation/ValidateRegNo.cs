using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Web;  
using System.ComponentModel.DataAnnotations;  
using System.Text.RegularExpressions;
using R.BAL;

namespace R.BusinessEntities
{
    public class CustomRegNoValidator : ValidationAttribute
    {
        iVehicleService _ivehicleservice;
        public CustomRegNoValidator(iVehicleService ivehicleservice) 
        {
            this._ivehicleservice = ivehicleservice;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string regno = value.ToString();
                if (FnCheckRegistraion(regno))
                {
                    return ValidationResult.Success;
                }
                else 
                {
                    return new ValidationResult("Already existed");
                }

                //string email = value.ToString();

                //if (Regex.IsMatch(email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase))
                //{
                //    return ValidationResult.Success;
                //}
                //else
                //{
                //    return new ValidationResult("Please Enter a Valid Email.");
                //}
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }

        private bool FnCheckRegistraion(string regno)
        {
            var result = false;
            regno = FnFormatRegistrtionNo(regno);
            var isVehicleregno = _ivehicleservice.GetAllVehicles().Where(s => s.vehicleregistraionno == regno);

            if (isVehicleregno.Any())
            {
                result = true;
            }

            return result;
        }

        private string FnFormatRegistrtionNo(string regno)
        {
            regno = regno.Trim();
            regno = regno.ToUpper();
            regno = regno.Replace(" ", string.Empty);

            return regno;
        }
    }
}  
          