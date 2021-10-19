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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WaiterRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<WaiterDto> CreateWaiter(CreateWaiterDto waiter)
        {
            var waiterToCreate = _mapper.Map<Waiter>(waiter);
            _context.Waiters.Add(waiterToCreate);
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToCreate));
        }

        public Task DeleteWaiter(Guid waiterId)
        {
            var waiterToDelete = _context.Waiters.FirstOrDefault(t => t.Id == waiterId);
            if (waiterToDelete == null)
            {
                throw new ArgumentException($"Waiter with id:{waiterId} not found.");
            }

            _context.Waiters.Remove(waiterToDelete);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<WaiterDto> GetWaiterById(Guid waiterId)
        {
            var waiterToReturn = _context.Waiters.FirstOrDefault(t => t.Id == waiterId);
            if (waiterToReturn == null)
            {
                throw new ArgumentException($"Waiter with id:{waiterId} not found.");
            }

            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToReturn));
        }

        public Task<IEnumerable<WaiterDto>> GetAllWaiters()
        {
            return Task.Run(() => _mapper.Map<IEnumerable<WaiterDto>>(_context.Waiters));
        }

        public Task<IEnumerable<WaiterDto>> CreateNWaiters(int nr)
        {
            var waiters = new List<Waiter>();
            while (nr > 0)
            {
                waiters.Add(_context.Waiters.Add(new Waiter { IsFree = true }).Entity);
                nr--;
            }

            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<IEnumerable<WaiterDto>>(waiters));
        }

        public Task<WaiterDto> UpdateWaiter(WaiterDto waiter)
        {
            var waiterToUpdate = _context.Waiters.FirstOrDefault(w => w.Id == waiter.Id);
            if (waiterToUpdate == null)
            {
                throw new ArgumentException($"Waiter with id: {waiter.Id} not found");
            }

            waiterToUpdate.IsFree = waiter.IsFree;
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<WaiterDto>(waiterToUpdate));
        }
    }
}
