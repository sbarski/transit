using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transit.Controllers
{
    public class HomeController : Controller
    {
        private IAsyncDocumentSession _documentSession;

        public HomeController(IAsyncDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public ActionResult Index()
        {
            var documentSession = _documentSession == null;

            return View();
        }
    }
}
