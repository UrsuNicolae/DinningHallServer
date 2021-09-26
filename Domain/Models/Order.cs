using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class Order
    {
        public Order(IEnumerable<Food> foods)
        {
            Foods = foods;
        }
        public IEnumerable<Food> Foods { get; set; }
    }
}
