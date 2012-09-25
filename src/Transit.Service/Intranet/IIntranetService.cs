using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Intranet;
using Transit.Web.Model;

namespace Transit.Service.Intranet
{
    public interface IIntranetService
    {
        void AddStaff(SignUp staff);

        IEnumerable<Staff> GetStaffList();

        void SetStaffLocation(IList<Staff> staff);
    }
}
