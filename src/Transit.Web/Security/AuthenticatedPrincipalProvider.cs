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
    public class AuthenticatedPrincipalProvider
    {
        private readonly IDocumentStore _documentStore;

        public AuthenticatedPrincipalProvider(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IPrincipal CreatePrincipal(string username, string password)
        {
            using (var session = _documentStore.OpenSession())
            {
                User user = session.Query<User>()
                    .Where(m => m.Username == username && m.PasswordHash == password)
                    .SingleOrDefault();

                if (user != null)
                {
                    var identity = new GenericIdentity(username);
                    var principal = new GenericPrincipal(identity, new[] { "User" });
                    return principal;
                }

                return null;
            }
        }
    }
}
