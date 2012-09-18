using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Core;
using Transit.Web.Security;

namespace Transit.Web.Account
{
    public class UserManagement
    {
        public static void SignInUser(User user)
        {
            System.Web.HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] { });
        }
    }
}
