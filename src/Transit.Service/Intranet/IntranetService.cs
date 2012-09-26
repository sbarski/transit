using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Transit.Domain.Intranet;
using Transit.Web.Attributes;
using Transit.Web.Model;

namespace Transit.Service.Intranet
{
    public class IntranetService : IIntranetService
    {
        private readonly IDocumentStore _documentStore;

        public IntranetService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IEnumerable<Staff> GetStaffList()
        {
            using (var db = _documentStore.OpenSession())
            {
                var staff = db.Query<Staff>()
                    .OrderBy(m => m.Out)
                    .OrderBy(m => m.Lastname)
                    .OrderBy(m => m.Firstname)
                    .AsEnumerable();

                return staff;
            }
        }

        public void SetStaffLocation(IList<Staff> staff)
        {
            if (staff.Count == 0)
            {
                return;
            }

            using (var db = _documentStore.OpenSession())
            {
                foreach (var member in staff)
                {
                    var employee = db.Load<Staff>(member.Id);

                    employee.In = member.In;
                    employee.Out = member.Out;
                    employee.Location = member.Location;
                }

                db.SaveChanges();
            }
        }

        public void AddStaff(SignUp staff)
        {
            var user = new Staff() { Firstname = staff.FirstName, Lastname = staff.LastName, In = null, Out = null, IsOut = false, Location = "Melbourne kOffice" };

            using (var db = _documentStore.OpenSession())
            {
                db.Store(user);
                db.SaveChanges();
            }
        }
    }
}
