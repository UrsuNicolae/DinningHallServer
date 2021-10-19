using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderDto> CreateOrder(CreateOrderDto order);

        Task DeleteOrder(Guid orderId);

        Task<OrderDto >GetOrderById(Guid orderId);

        Task<IEnumerable<OrderDto>> CreateOrders(IEnumerable<CreateOrderDto> orders);

        Task<OrderDto> GetOrderByTableId(Guid tableId);

    }
}
