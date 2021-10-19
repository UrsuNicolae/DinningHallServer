using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface IWaiterRepository
    {
        Task<WaiterDto> CreateWaiter(CreateWaiterDto waiter);

        Task DeleteWaiter(Guid waiterId);

        Task<WaiterDto> GetWaiterById(Guid waiterId);

        Task<IEnumerable<WaiterDto>> GetAllWaiters();

        Task<IEnumerable<WaiterDto>> CreateNWaiters(int nr);

        Task<WaiterDto> UpdateWaiter(WaiterDto waiter);
    }
}
