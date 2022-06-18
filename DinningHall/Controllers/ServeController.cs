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
                    GenerateOrder(table.Id);
                    UpdateTable(table.Id);
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
                        var tableId = waiter.ServeTable(_httpClient, _mapper);

                        new Thread(() => {
                            GenerateOrder(tableId);
                            UpdateTable(tableId);
                        }).Start();
                    }

                });
            }
        }

        #region helpers

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
        #endregion
    }
}
