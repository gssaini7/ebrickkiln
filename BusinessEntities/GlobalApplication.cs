using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.BusinessEntities
{
    public class GlobalApplication:ientity
    {
        public long global_app_id { get; set; }
        public Nullable<long> local_app_id { get; set; }
        public string app_type { get; set; }
        public Nullable<long> templocal_app_id { get; set; }
        public Nullable<int> currentversion { get; set; }
    }

    public class ientity 
    {
        public Guid guidid { get; set; }
    }
}
