using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raven.Client;

namespace Transit.Controllers
{
    public class UserController : ApiController
    {
        private IAsyncDocumentSession _asyncDocumentSession;

        public UserController(IAsyncDocumentSession asyncDocumentSession)
        {
            _asyncDocumentSession = asyncDocumentSession;
        }


    }
}
