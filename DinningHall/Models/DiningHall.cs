using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public sealed class DiningHall
    {
        public IEnumerable<Table> Tables { get; set; }
    }
}
