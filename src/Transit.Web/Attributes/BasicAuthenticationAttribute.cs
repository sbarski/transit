using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Core;
using Transit.Infrastructure.Security;
using Transit.Web.Account;
using Transit.Web.Security;

namespace Transit.Web.Attributes
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private readonly IDocumentStore _documentStore;
        public BasicAuthenticationAttribute(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

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

                using (var db = _documentStore.OpenSession())
                {
                    var user = db.Query<User>()
                        .Where(m => HashHelper.CompareHash(m.Token, decodedTokenHash, HashHelper.HashType.SHA512))
                        .SingleOrDefault();

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
