using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace DinningHall.Models
{
    public class Order
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid TableId { get; set; }
        public Table Table { get; set; }
        
        public IList<Food> Foods { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Priority { get; set; }

        [Required]
        public double MaxWaitTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
