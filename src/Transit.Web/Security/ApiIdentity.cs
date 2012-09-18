using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Transit.Domain.Core;

namespace Transit.Web.Security
{
    public class ApiIdentity : IIdentity
    {
        public User User { get; private set; }

        public ApiIdentity(User user) 
        {
            if (user == null)
            {
                throw new ArgumentNullException("User is null");
            }

            User = user;
        }

        public string Name
        {
            get { return User.Email; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}
