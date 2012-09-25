using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transit.Domain.Intranet;
using Transit.Service.Intranet;
using Transit.Web.Attributes;

namespace Transit.Controllers
{
    [BasicAuthentication]
    public class IntranetController : ApiController
    {
        private readonly IIntranetService _intranetService;
        public IntranetController(IIntranetService intranetService)
        {
            _intranetService = intranetService;
        }

        public IEnumerable<Staff> Staff()
        {
            return _intranetService.GetStaffList();
        }

        public void SetStaffLocation(IList<Staff> staff)
        {
            _intranetService.SetStaffLocation(staff);
        }
    }
}
