using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Domain.Core
{
    public class User : AuditedEntity
    {
        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id {get;set;}

        //General UserInformation
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        /// <summary>
        /// Api Token for mobile user
        /// </summary>
        public string Token {get;set;}
    }
}
