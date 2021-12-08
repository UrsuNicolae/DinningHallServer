using DinningHall.Data.Interfaces;
using DinningHall.DTOs;
using DinningHall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Http;
using DinningHall.Models.Enums;

namespace DinningHall.Controllers
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
        private readonly IMapper _mapper;

        private static double reputation = 0;
        private static int nrSent = 0;
        private const int maxWait = 30;//seconds
        private bool canSendOrders = true;

        public ServeController(
            IOrderRepository orderRepo,
            ITableRepository tableRepo,
            IWaiterRepository waiterRepo,
            IFoodRepository foodRepo,
            IHttpDataClient httpClient,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _tableRepo = tableRepo;
            _waiterRepo = waiterRepo;
            _foodRepo = foodRepo;
            _httpClient = httpClient;
            _mapper = mapper;
        }


        [HttpPost]
        public ActionResult StartSimulation()
        {
            var tables = _tableRepo.GetAllTables().Result;
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
            canSendOrders = true;
            while (canSendOrders)
            {
                Console.WriteLine($"--> Reputation is {reputation}");
                foreach (var waiter in await _waiterRepo.GetAllWaiters())
                {
                    if (waiter.IsFree)
                    {
                        var table = (await _tableRepo.GetAllTables()).FirstOrDefault(t => t.TableStatus == TableStatus.WaitToOrder &&
                                                          !t.IsFree);
                        if (table != null)
                        {
                            waiter.IsFree = false;
                            await _waiterRepo.UpdateWaiter(waiter);
                            table.TableStatus = TableStatus.WaitToBeServed;
                            await _tableRepo.UpdateTable(table);
                            var sentAt = DateTime.UtcNow;
                            nrSent++;
                            var response = await _httpClient.SendOrder(_mapper.Map<OrderDto>(table.Order));
                            if (response.IsSuccessStatusCode)
                            {
                                var receivedAfter = DateTime.UtcNow - sentAt;
                                if (receivedAfter.Seconds < maxWait)
                                {
                                    reputation = (reputation + 5) / nrSent;
                                }
                                else if (receivedAfter.Seconds < maxWait * 1.1)
                                {
                                    reputation = (reputation + 4) / nrSent;
                                }
                                else if (receivedAfter.Seconds < maxWait * 1.2)
                                {
                                    reputation = (reputation + 3) / nrSent;
                                }
                                else if (receivedAfter.Seconds < maxWait * 1.3)
                                {
                                    reputation = (reputation + 2) / nrSent;
                                }
                                else if (receivedAfter.Seconds < maxWait * 1.4)
                                {
                                    reputation = (reputation + 1) / nrSent;
                                }

                                waiter.IsFree = true;
                                await _waiterRepo.UpdateWaiter(waiter);
                                GenerateOrder(table.Id);
                                UpdateTable(table.Id);
                            }
                        }
                    }
                }
            }
        }

        #region helpers

        private void GenerateOrder(Guid tableId)
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
                Priority = (byte)new Random().Next(1, 5),
                TableId = tableId,
                CreatedAt = DateTime.UtcNow
            });

        }

        private void UpdateTable(Guid tableId)
        {
            var tableFromRepo = _tableRepo.GetTableById(tableId).Result;
            tableFromRepo.IsFree = false;
            tableFromRepo.TableStatus = TableStatus.WaitToOrder;
            _tableRepo.UpdateTable(tableFromRepo);
        }
        #endregion
    }
}
