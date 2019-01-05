using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace R.BusinessEntities
{
    public class LicenceManufaturerModel
    {
        
        public long Local_app_id { get; set; }
        public Nullable<long> App_User_id { get; set; }
        public Nullable<long> Concern_address_id { get; set; }

        [Required]
        public string primise_type { get; set; }

        [Display(Name = "Date of establishment")]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MMM/dd/yyyy}")]
        public Nullable<System.DateTime> date_of_establishment { get; set; }
        public string registration_type { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public string registration_number { get; set; }
        public string nature_manufctur_present { get; set; }
        public string type_wm { get; set; }
        public Nullable<int> skilled_mp { get; set; }
        public Nullable<int> semiskilled_mp { get; set; }
        public Nullable<int> unskilled_mp { get; set; }
        public Nullable<int> specialist_mp { get; set; }
        public string monogram_trade_mark { get; set; }
        public string detail_machinery { get; set; }
        public string detail_workshop_facilty { get; set; }
        public string facilities_testing { get; set; }
        public string electric_energy { get; set; }
        public string banker_name { get; set; }
        public string tax_reg_type { get; set; }
        public string tax_reg_no { get; set; }
        public string previous_apply_details { get; set; }
        public string within_state_outside { get; set; }
        public string model_aprvl_goi { get; set; }
        public Nullable<System.DateTime> desired_inspection_Date { get; set; }
        public string place { get; set; }
        public Nullable<System.DateTime> app_date { get; set; }
        public string detail_loan_amt { get; set; }
        public string detail_loan_acount_no { get; set; }
        public string ConcernAddrsDoc { get; set; }
        public Nullable<int> ConcernAddrsDocSize { get; set; }
        public Nullable<System.DateTime> app_update_date { get; set; }
        public string type_wm_weights { get; set; }
        public string type_wm_measures { get; set; }
        public string type_wm_weighnginstru { get; set; }
        public string type_wm_measurnginstru { get; set; }
        public string namesignapplicant { get; set; }
        public string designationapplicant { get; set; }


        public Address MainAddress { get; set; }



        public GlobalApplication GlobalAppLocalRel { get; set; }
    }
}
