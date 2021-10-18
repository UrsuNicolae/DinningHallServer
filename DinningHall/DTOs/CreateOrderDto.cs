﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;

namespace DinningHall.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public IList<Food> Foods { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Priority { get; set; }

        [Required]
        public int MaxWaitTime { get; set; }
    }
}
