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

        public async Task<Guid> ServeTable(IHttpDataClient httpClient, IMapper mapper)
        {
            Guid tableId = Guid.Empty;
            new Thread(async () =>
            {
                var table = StaticContext.Tables.FirstOrDefault(t =>
                          t.TableStatus == TableStatus.WaitToOrder &&
                          !t.IsFree);
                if (table != null)
                {
                    IsFree = false;

                    table.TableStatus = TableStatus.WaitToBeServed;
                    var sentAt = DateTime.UtcNow;
                    StaticContext.NRSet++;
                    Console.WriteLine($"--> Reputation is {StaticContext.Reputation}");
                    var response = await httpClient.SendOrder(mapper.Map<OrderDto>(table.Order));
                    if (response.IsSuccessStatusCode)
                    {
                        var receivedAfter = DateTime.UtcNow - sentAt;
                        if (receivedAfter.Seconds < StaticContext.MaxWait)
                        {
                            StaticContext.Reputation = (StaticContext.Reputation + 5) / StaticContext.NRSet;
                        }
                        else if (receivedAfter.Seconds < StaticContext.MaxWait * 1.1)
                        {
                            StaticContext.Reputation = (StaticContext.Reputation + 4) / StaticContext.NRSet;
                        }
                        else if (receivedAfter.Seconds < StaticContext.MaxWait * 1.2)
                        {
                            StaticContext.Reputation = (StaticContext.Reputation + 3) / StaticContext.NRSet;
                        }
                        else if (receivedAfter.Seconds < StaticContext.MaxWait * 1.3)
                        {
                            StaticContext.Reputation = (StaticContext.Reputation + 2) / StaticContext.NRSet;
                        }
                        else if (receivedAfter.Seconds < StaticContext.MaxWait * 1.4)
                        {
                            StaticContext.Reputation = (StaticContext.Reputation + 1) / StaticContext.NRSet;
                        }

                        IsFree = true;
                        tableId = table.Id;
                    }
                }
            }).Start();
            return tableId;
        }
    }
}
