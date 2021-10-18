using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models.Enums;

namespace DinningHall.DTOs
{
    public class GetTableDto
    {
        public Guid Id { get; set; }

        public bool IsFree { get; set; }

        public TableStatus TableStatus { get; set; }

        public GetOrderDto? Order { get; set; }
    }
}
