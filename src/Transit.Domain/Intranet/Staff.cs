using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Domain.Intranet
{
    public class Staff
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Location { get; set; }

        public bool IsOut { get; set; }

        public DateTime? Out { get; set; }
        public DateTime? In { get; set; }
    }
}
