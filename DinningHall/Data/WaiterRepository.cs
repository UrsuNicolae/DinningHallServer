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
        public Task<GetWaiterDto> CreateWaiter(CreateWaiterDto waiter)
        {
            var waiterToCreate = _mapper.Map<Waiter>(waiter);
            _context.Waiters.Add(waiterToCreate);
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<GetWaiterDto>(waiterToCreate));
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

        public Task<GetWaiterDto> GetWaiterById(Guid waiterId)
        {
            var waiterToReturn = _context.Waiters.FirstOrDefault(t => t.Id == waiterId);
            if (waiterToReturn == null)
            {
                throw new ArgumentException($"Waiter with id:{waiterId} not found.");
            }

            return Task.Run(() => _mapper.Map<GetWaiterDto>(waiterToReturn));
        }

        public Task<IEnumerable<GetWaiterDto>> GetAllWaiters()
        {
            return Task.Run(() => _mapper.Map<IEnumerable<GetWaiterDto>>(_context.Waiters));
        }

        public Task<IEnumerable<GetWaiterDto>> CreateNWaiters(int nr)
        {
            var waiters = new List<Waiter>();
            while (nr > 0)
            {
                waiters.Add(_context.Waiters.Add(new Waiter { IsFree = true}).Entity);
                nr--;
            }

            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<IEnumerable<GetWaiterDto>>(waiters));
        }
    }
}
