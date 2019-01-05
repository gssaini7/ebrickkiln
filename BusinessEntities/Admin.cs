using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.BusinessEntities
{
    public class Adminentity
    {
        public long admin_id { get; set; }
        public string admin_username { get; set; }
        public string admin_password { get; set; }
        public string admin_type { get; set; }
        public Nullable<long> admin_district_circle_id { get; set; }
        public Nullable<long> admin_address_id { get; set; }
        public Nullable<int> isActiveFlag { get; set; }
        public string uniqueida { get; set; }

        public virtual Address adminaddress { get; set; }
    }
}
