using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transit.Domain.Core;
using Transit.Infrastructure.Security;
using Transit.Web.Account;
using Transit.Web.Model;

namespace Transit.Service.Account
{
    public class AccountService : IAccountService
    {
        private readonly IDocumentStore _documentStore;

        public AccountService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public SignIn SignUp(SignUp model)
        {
            var salt = SecurityHelper.GenerateSalt();

            var passwordHash = SecurityHelper.GeneratePasswordHash(model.Password+salt);

            var securityToken = SecurityHelper.GenerateSecurityToken();

            var user = new User(model.Email, passwordHash) { Salt = salt, FirstName = model.FirstName, LastName = model.LastName, Token = securityToken };

            using (var db = _documentStore.OpenSession())
            {
                db.Store(user);
                db.SaveChanges();
            }

            return SignInUser(user);
        }

        public SignIn LogOn(LogOn model)
        {
            using (var db = _documentStore.OpenSession())
            {
                var user = db.Query<User>()
                            .Where(m => m.Email == model.Email) 
                            .SingleOrDefault();

                if (user != null && HashHelper.CompareHash(model.Password + user.Salt, user.PasswordHash))
                {
                    return SignInUser(user);
                }
            }

            return null;
        }

        private SignIn SignInUser(User user)
        {
            UserManagement.SignInUser(user);

            return new SignIn() { FirsName = user.FirstName, LastName = user.LastName, Id = user.Id, Token = user.Token };
        }
    }
}
