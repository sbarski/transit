using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transit.Domain.Intranet;
using Transit.Web.Attributes;

namespace Transit.Controllers
{
    public class IntranetController : ApiController
    {
        [BasicAuthentication]
        public IEnumerable<Staff> Staff()
        {
            return null;
        }
    }
}
