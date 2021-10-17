using System;
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
        public Guid Id { get; }

        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan PreparationTime { get; set; }

        [Required]
        public int Complexity { get; set; }

        [Required]
        public CookingApparatuses CookingApparatus { get; set; }
    }
}
