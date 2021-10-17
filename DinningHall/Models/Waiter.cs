using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models.Enums;

namespace DinningHall.Models
{
    public sealed class Waiter
    {
        public Waiter()
        {
            Id = new Guid();
            IsFree = true;
        }
        public Guid Id { get; set; }

        public bool IsFree { get; set; }

        public (Guid, Order) FindTableToServe(IEnumerable<Table> tables)
        {
            IsFree = false;
            foreach (var table in tables)
            {
                if (table.IsFree && table.TableStatus == TableStatus.WaitToOrder)
                {
                    table.TableStatus = TableStatus.WaitToBeServed;
                    table.IsFree = false;
                    return (table.Id, table.GenerateOrder());
                }
            }

            IsFree = true;
            return (Guid.Empty, null);
        }
    }
}
