using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Data;
using DinningHall.DTOs;
using DinningHall.Http;
using DinningHall.Models.Enums;

namespace DinningHall.Models
{
    public class Waiter
    {
        public Guid Id { get; set; }

        public bool IsFree { get; set; }

        public void ServeTable(IHttpDataClient httpClient, IMapper mapper)
        {
            new Thread(() =>
            {
                var table = StaticContext.Tables.FirstOrDefault(t =>
                          t.TableStatus == TableStatus.WaitToOrder &&
                          !t.IsFree);
                if (table != null)
                {

                    table.TableStatus = TableStatus.WaitToBeServed;
                    var sentAt = DateTime.UtcNow;
                    StaticContext.NRSet++;
                    Console.WriteLine($"--> Reputation is {StaticContext.Reputation}");
                    table.Order.WaiterId = Id;
                    httpClient.SendOrder(new 
                        {
                            order = table.Order,
                            tableId = table.Id,
                            watiterId = Id
                        }, table.Order.Id);
                    
                }
            }).Start();
        }
    }
}
