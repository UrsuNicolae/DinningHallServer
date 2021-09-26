using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class Table
    {
        public Table()
        {
            Id = new Guid();
            IsFree = true;
            TableStatus = TableStatus.WaitToOrder;
        }

        public Guid Id { get;}

        public bool IsFree  { get; set; }

        public TableStatus TableStatus { get; set; }

        public Order GenerateOrder()
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
            }

            return new Order(foods);
        }
    }
}
