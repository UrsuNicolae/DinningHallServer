using DinningHall.Data.Interfaces;
using DinningHall.DTOs;
using DinningHall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Http;
using DinningHall.Models.Enums;

namespace Dinning_Hall.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServeController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ITableRepository _tableRepo;
        private readonly IWaiterRepository _waiterRepo;
        private readonly IFoodRepository _foodRepo;
        private readonly IHttpDataClient _httpClient;

        public ServeController(
            IOrderRepository orderRepo,
            ITableRepository tableRepo,
            IWaiterRepository waiterRepo,
            IFoodRepository foodRepo,
            IHttpDataClient httpClient)
        {
            _orderRepo = orderRepo;
            _tableRepo = tableRepo;
            _waiterRepo = waiterRepo;
            _foodRepo = foodRepo;
            _httpClient = httpClient;
        }
        

        [HttpGet]
        public ActionResult StartSimulation()
        {
            var tables = _tableRepo.GetAllTables().Result;
            foreach (var table in tables)
            {
                var rnd = new Random();
                if (rnd.Next(0, 1) == 1)
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
            var tables = _tableRepo.GetAllTables().Result;
            var waiters = _waiterRepo.GetAllWaiters().Result;
            while (true)
            {
                foreach (var waiter in waiters)
                {
                    if (waiter.IsFree)
                    {
                        var table = tables.FirstOrDefault(t => t.TableStatus == TableStatus.WaitToOrder);
                        if (table != null)
                        {
                            waiter.IsFree = false;
                            _waiterRepo.UpdateWaiter(waiter);
                            _httpClient.SendOrder(table.Order)
                        }
                    }
                    async Task<string> func()
                    {
                        var response = await client.GetAsync("posts/" + post);
                        return await response.Content.ReadAsStringAsync();
                    }

                    tasks.Add(func());
                }
                await Task.WhenAll(tasks);

                if (waiters.Any(w => w.IsFree))
                {
                    var waiter = waiters.FirstOrDefault(w => w.IsFree);
                    waiter.IsFree = false;
                    if (tables.Any(t => !t.IsFree && t.TableStatus == TableStatus.WaitToOrder))
                    {
                        var table = tables.FirstOrDefault(t => !t.IsFree && t.TableStatus == TableStatus.WaitToOrder);
                        waiter = _waiterRepo.UpdateWaiter(waiter).Result;
                        Task.Delay(2000);//todo create call
                        waiter.IsFree = true;
                        waiter = _waiterRepo.UpdateWaiter(waiter).Result;

                    }
                    //var tb = waiter?.FindTableToServe(tables);;
                    
                }
            }

        }

        #region helpers

        private void  GenerateOrder(Guid tableId)
        {
            var nrOfFoods = new Random().Next(1, 10);
            var foodsFromDb = _foodRepo.GetAllFoods().Result.ToList();
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

            _orderRepo.CreateOrder(new CreateOrderDto
            {
                Foods = foods,
                MaxWaitTime = highestPreparationTime * 1.3,
                Priority = (byte) new Random().Next(1, 5),
                TableId = tableId,
                CreatedAt = DateTime.UtcNow
            });

        }

        private void UpdateTable(Guid tableId)
        {
            var tableFromRepo = _tableRepo.GetTableById(tableId).Result;
            tableFromRepo.IsFree = false;
            _tableRepo.UpdateTable(tableFromRepo);
        }
        #endregion
    }
}
