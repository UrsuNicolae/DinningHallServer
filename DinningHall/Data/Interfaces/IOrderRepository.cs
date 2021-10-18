using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<GetOrderDto> CreateOrder(CreateOrderDto order);

        Task DeleteOrder(Guid orderId);

        Task<GetOrderDto >GetOrderById(Guid orderId);

        Task<IEnumerable<GetOrderDto>> CreateOrders(IEnumerable<CreateOrderDto> orders);

        Task<GetOrderDto> GetOrderByTableId(Guid tableId);

    }
}
