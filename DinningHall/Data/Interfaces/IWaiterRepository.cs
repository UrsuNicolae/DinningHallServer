using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface IWaiterRepository
    {
        Task<GetWaiterDto> CreateWaiter(CreateWaiterDto waiter);

        Task DeleteWaiter(Guid waiterId);

        Task<GetWaiterDto> GetWaiterById(Guid waiterId);

        Task<IEnumerable<GetWaiterDto>> GetAllWaiters();

        Task<IEnumerable<GetWaiterDto>> CreateNWaiters(int nr);
    }
}
