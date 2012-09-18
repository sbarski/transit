using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Web.Model
{
    public class SignIn
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public string FirsName { get; set; }
        public string LastName { get; set; }
    }
}
