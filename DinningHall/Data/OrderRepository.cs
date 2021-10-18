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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper _mapper)
        {
            _context = context;
            this._mapper = _mapper;
        }
        public Task<GetOrderDto> CreateOrder(CreateOrderDto order)
        {
            var orderToReturn = _context.Orders.Add(_mapper.Map<Order>(order));
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<GetOrderDto>(orderToReturn));
        }

        public Task DeleteOrder(Guid orderId)
        {
            var orderToDelete = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (orderToDelete == null)
            {
                throw new ArgumentException($"Order with id:{orderId} not found.");
            }

            _context.Orders.Remove(orderToDelete);
            return Task.Run(() => _context.SaveChanges());
        }

        public Task<GetOrderDto> GetOrderById(Guid orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with id:{orderId} not found.");
            }

            return Task.Run(() => _mapper.Map<GetOrderDto>(order));
        }

        public Task<IEnumerable<GetOrderDto>> CreateOrders(IEnumerable<CreateOrderDto> orders)
        {
            var ordersToCreate = _mapper.Map<Order>(orders);
            _context.Orders.AddRange(ordersToCreate);
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<IEnumerable<GetOrderDto>>(ordersToCreate));
        }

        public Task<GetOrderDto> GetOrderByTableId(Guid tableId)
        {
            var table = _context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (table == null)
            {
                throw new ArgumentException($"Table with Id:{tableId} not found.");
            }

            return Task.Run(() => _mapper.Map<GetOrderDto>(table.Order));
        }
    }
}
