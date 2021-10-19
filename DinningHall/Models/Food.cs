﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models.Enums;

namespace DinningHall.Models
{
    public class Food
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public int Complexity { get; set; }

        [Required]
        public CookingApparatuses CookingApparatus { get; set; }
    }
}
