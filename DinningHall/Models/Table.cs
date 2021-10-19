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

        /*public Order GenerateOrder()
        {
            var nrOfFoods = new Random().Next(1, 10);
            var foods = new List<Food>();
            while (nrOfFoods > 0)
            {
                var name = "Order" + new Random().Next(0, 1000);
                var prepTime = TimeSpan.FromMinutes(new Random().Next(1, 60));
                var complexity = new Random().Next(1, 10);
                var cookingApparatus = (CookingApparatuses)Enum.Parse(typeof(CookingApparatuses), new Random().Next(0, 2).ToString());
                foods.Add(new Food(name, prepTime, complexity, cookingApparatus));
                nrOfFoods--;
            }

            return new Order(foods);
        }*/
    }
}
