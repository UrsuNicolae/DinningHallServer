using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Data.Interfaces;
using DinningHall.DTOs;
using DinningHall.Models;

namespace DinningHall.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public OrderRepository(DbContextFactory contextFactory, IMapper _mapper)
        {
            _contextFactory = contextFactory;
            this._mapper = _mapper;
        }
        public Task<OrderDto> CreateOrder(CreateOrderDto order)
        {
            var context = _contextFactory.Create();
            var orderToReturn = context.Orders.Add(_mapper.Map<Order>(order)).Entity;
            SaveChanges(context);
            return Task.Run(() => _mapper.Map<OrderDto>(orderToReturn));
        }

        public Task DeleteOrder(Guid orderId)
        {
            var context = _contextFactory.Create();
            var orderToDelete = context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (orderToDelete == null)
            {
                throw new ArgumentException($"Order with id:{orderId} not found.");
            }

            context.Orders.Remove(orderToDelete);
            return Task.Run(() => context.SaveChanges());
        }

        public Task<OrderDto> GetOrderById(Guid orderId)
        {
            var context = _contextFactory.Create();
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with id:{orderId} not found.");
            }

            return Task.Run(() => _mapper.Map<OrderDto>(order));
        }

        public Task<IEnumerable<OrderDto>> CreateOrders(IEnumerable<CreateOrderDto> orders)
        {
            var context = _contextFactory.Create();
            var ordersToCreate = _mapper.Map<Order>(orders);
            context.Orders.AddRange(ordersToCreate);
            SaveChanges(context);
            return Task.Run(() => _mapper.Map<IEnumerable<OrderDto>>(ordersToCreate));
        }

        public Task<OrderDto> GetOrderByTableId(Guid tableId)
        {
            var context = _contextFactory.Create();
            var table = context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (table == null)
            {
                throw new ArgumentException($"Table with Id:{tableId} not found.");
            }

            return Task.Run(() => _mapper.Map<OrderDto>(table.Order));
        }

        public void SaveChanges(AppDbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                //already saved
            }
        }
    }
}
