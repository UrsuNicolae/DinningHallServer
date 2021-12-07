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
    public class WaiterRepository : IWaiterRepository
    {
        private readonly DbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public WaiterRepository(DbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public Task<WaiterDto> CreateWaiter(CreateWaiterDto waiter)
        {
            var context = _contextFactory.Create();
            var waiterToCreate = _mapper.Map<Waiter>(waiter);
            context.Waiters.Add(waiterToCreate);
            context.SaveChanges();
            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToCreate));
        }

        public Task DeleteWaiter(Guid waiterId)
        {
            var context = _contextFactory.Create();
            var waiterToDelete = context.Waiters.FirstOrDefault(t => t.Id == waiterId);
            if (waiterToDelete == null)
            {
                throw new ArgumentException($"Waiter with id:{waiterId} not found.");
            }

            context.Waiters.Remove(waiterToDelete);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<WaiterDto> GetWaiterById(Guid waiterId)
        {
            var context = _contextFactory.Create();
            var waiterToReturn = context.Waiters.FirstOrDefault(t => t.Id == waiterId);
            if (waiterToReturn == null)
            {
                throw new ArgumentException($"Waiter with id:{waiterId} not found.");
            }

            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToReturn));
        }

        public Task<IEnumerable<WaiterDto>> GetAllWaiters()
        {
            var context = _contextFactory.Create();
            return Task.Run(() => _mapper.Map<IEnumerable<WaiterDto>>(context.Waiters));
        }

        public Task<IEnumerable<WaiterDto>> CreateNWaiters(int nr)
        {
            var context = _contextFactory.Create();
            var waiters = new List<Waiter>();
            while (nr > 0)
            {
                waiters.Add(context.Waiters.Add(new Waiter { IsFree = true }).Entity);
                nr--;
            }

            context.SaveChanges();
            return Task.Run(() => _mapper.Map<IEnumerable<WaiterDto>>(waiters));
        }

        public Task<WaiterDto> UpdateWaiter(WaiterDto waiter)
        {
            var context = _contextFactory.Create();
            var waiterToUpdate = context.Waiters.FirstOrDefault(w => w.Id == waiter.Id);
            if (waiterToUpdate == null)
            {
                throw new ArgumentException($"Waiter with id: {waiter.Id} not found");
            }

            waiterToUpdate.IsFree = waiter.IsFree;
            context.SaveChanges();
            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToUpdate));
        }
    }
}
