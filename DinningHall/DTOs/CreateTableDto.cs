using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;
using DinningHall.Models.Enums;

namespace DinningHall.DTOs
{
    public class CreateTableDto
    {
        [Required]
        public bool IsFree { get; set; }

        [Required]
        public TableStatus TableStatus { get; set; }

        public CreateOrderDto? Order { get; set; }
    }
}
