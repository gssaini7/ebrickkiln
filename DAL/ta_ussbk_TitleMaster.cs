//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ta_ussbk_TitleMaster
    {
        public ta_ussbk_TitleMaster()
        {
            this.ta_ussbk_Description = new HashSet<ta_ussbk_Description>();
        }
    
        public int titleid { get; set; }
        public string titlename { get; set; }
    
        public virtual ICollection<ta_ussbk_Description> ta_ussbk_Description { get; set; }
    }
}
