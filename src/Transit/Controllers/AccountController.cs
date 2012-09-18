using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transit.Service.Account;
using Transit.Web.Model;

namespace Transit.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public SignIn SignUp(SignUp model)
        {
           return _accountService.SignUp(model);
        }

        public SignIn LogOn(LogOn model)
        {
            var logon = _accountService.LogOn(model);

            if (logon == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "User Unauthorized"));
            }

            return logon;
        }
    }
}
