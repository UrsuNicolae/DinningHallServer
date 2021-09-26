using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class Table
    {
        public Guid Id { get; set; }

        public bool IsFree  { get; set; }

        public DateTime OrderedAt { get; set; }

        public TimeSpan WaitToOrder { get; set; }

        public TimeSpan WaitToBeServed { get; set; }  = TimeSpan.Zero;
    }
}
