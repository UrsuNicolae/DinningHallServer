using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class Order
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public int Complexity { get; set; }

        public CookingApparatuses Cooking_Apparatus { get; set; }
    }
}
