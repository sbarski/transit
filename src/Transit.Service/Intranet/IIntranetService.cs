using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Intranet;

namespace Transit.Service.Intranet
{
    interface IIntranetService
    {
        IEnumerable<Staff> GetStaffList();

        void SetStaffLocation(IList<Staff> staff);
    }
}
