using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DinningHall.Data;
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
        
        public Order? Order { get; set;} 

        public void GenerateTableOrder()
        {
            new Thread(() =>
            {
                GenerateOrder(Id);
                UpdateTable(Id);
            }).Start();
        }

        private void GenerateOrder(Guid tableId)
        {
            var nrOfFoods = new Random().Next(1, 10);
            var foodsFromDb = StaticContext.Foods;
            var foods = new List<Food>();
            var highestPreparationTime = int.MinValue;
            while (nrOfFoods > 0)
            {
                var foodToAdd = foodsFromDb.ElementAt(new Random().Next(0, 9));
                if (highestPreparationTime < foodToAdd.PreparationTime)
                {
                    highestPreparationTime = foodToAdd.PreparationTime;
                }
                foods.Add(foodToAdd);
                nrOfFoods--;
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Foods = foods,
                MaxWaitTime = highestPreparationTime * 1.3,
                Priority = (byte)new Random().Next(1, 5),
                TableId = tableId,
                CreatedAt = DateTime.UtcNow
            };

            StaticContext.Orders.Add(order);
            foreach (var table in StaticContext.Tables)
            {
                if (table.Id == tableId)
                {
                    table.Order = order;
                    break;
                }
            }

        }

        private void UpdateTable(Guid tableId)
        {
            foreach (var table in StaticContext.Tables)
            {
                if (table.Id == tableId)
                {
                    table.IsFree = false;
                    table.TableStatus = TableStatus.WaitToOrder;
                    break;
                }
            }
        }
    }
}
