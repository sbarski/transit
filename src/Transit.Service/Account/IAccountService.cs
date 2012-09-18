using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transit.Web.Model;

namespace Transit.Service.Account
{
    public interface IAccountService
    {
        SignIn LogOn(LogOn model);
        SignIn SignUp(SignUp model);
    }
}
