using Raven.Client;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Transit.Web.Attributes
{
    public class RavenSessionAttribute : FilterAttribute, IActionFilter
    {
        private Container _container;

        public RavenSessionAttribute(Container container)
        {
            _container = container;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                Debug.WriteLine(string.Format("Do not commit changes. An exception was thrown: {0}", filterContext.Exception));

                return;
            }

            if (_container == null)
            {
                Debug.WriteLine(string.Format("The injected container in the RavenSessionAttribute is not valid"));
            }

            using (var documentSession = _container.GetInstance<IDocumentSession>())
            {
                documentSession.SaveChanges();
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
