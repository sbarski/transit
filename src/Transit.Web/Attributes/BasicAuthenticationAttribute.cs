using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Transit.Domain.Core;
using Transit.Infrastructure.Security;
using Transit.Web.Account;
using Transit.Web.Security;

namespace Transit.Web.Attributes
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public IDocumentStore DocumentStore { get; set; }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Missing Authorization Header") };
            }
            else
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedTokenHash = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                using (var db = DocumentStore.OpenSession())
                {
                    var user = db.Query<User>()
                        .SingleOrDefault(m => m.Token == decodedTokenHash);

                    if (user != null)
                    {
                        UserManagement.SignInUser(user);
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
