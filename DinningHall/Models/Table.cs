using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models.Enums;

namespace DinningHall.Models
{
    public class Table
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool IsFree { get; set; }

        [Required]
        public TableStatus TableStatus { get; set; }
        
        public Order? Order { get; set; }
    }
}
