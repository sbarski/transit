using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Intranet;
using Transit.Web.Attributes;

namespace Transit.Service.Intranet
{
    public class IntranetService : IIntranetService
    {
        public IEnumerable<Staff> GetStaffList()
        {
            return new List<Staff>{new Staff() { Firstname = "Peter", Lastname = "Sbarski", IsOut = false, In = null, Out = null, Location = "Melbourne Office" }};
        }

        public void SetStaffLocation(IList<Staff> staff)
        {
        }
    }
}
