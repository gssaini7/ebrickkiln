using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.BusinessEntities
{
    public class District
    {
        public long district_id { get; set; }
        public string district_name { get; set; }
        public Nullable<long> areacircle_id { get; set; }
        public virtual AreaCircle areacircle { get; set; }
    }

    public class AreaCircle 
    {
        public long areacircle_id { get; set; }
        public string areacircle_name { get; set; }
    }
}
