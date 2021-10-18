using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;

namespace DinningHall.DTOs
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }

        public IList<Food> Foods { get; set; }
        
        public byte Priority { get; set; }

        public int MaxWaitTime { get; set; }
    }
}
