using DinningHall.DTOs;
using DinningHall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Data;
using DinningHall.Http;
using DinningHall.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DinningHall.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServeController : ControllerBase
    {
        private readonly IHttpDataClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ServeController(
            IHttpDataClient httpClient,
            IMapper mapper,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _configuration = configuration; ;
        }


        [HttpPost]
        public ActionResult StartSimulation()
        {
            var tables = StaticContext.Tables;
            var index = 0;
            foreach (var table in tables)
            {
                index++;
                if (index % 2 == 0)
                {
                    table.GenerateTableOrder();
                }
            }

            return Ok();
        }

        [HttpPost]
        public async Task StartSendingOrders()
        {
            Console.WriteLine($"{_configuration["KitchenUrl"]}");
            while (true)
            {
                Parallel.ForEach(StaticContext.Waiters, waiter =>
                {
                    if (waiter.IsFree)
                    {
                        waiter.IsFree = false;
                        waiter.ServeTable(_httpClient, _mapper);
                    }

                });
            }
        }

        [HttpPost]
        public async Task Distribution(OrderDetails order)
        {
            Console.Write($"Order {order.Id} is ready to be served"); //test

            var receivedAfter = DateTime.UtcNow - order.PickUpTime;
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
            var waiter = StaticContext.Waiters.FirstOrDefault(w => w.Id == order.WaiterId);
            waiter.IsFree = true;
            var table = StaticContext.Tables.FirstOrDefault(t => t.Id == order.TableId);
            table.GenerateTableOrder();
            Thread.Sleep(200);
        }
    }
}
