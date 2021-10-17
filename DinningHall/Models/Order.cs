using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public sealed class Order
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public IEnumerable<Food> Foods { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Priority { get; set; }

        [Required]
        public int MaxWaitTime { get; set; }
    }
}
