using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Data.Interfaces;
using DinningHall.Models;

namespace DinningHall.Data
{
    public class FoodRepository : IFoodRepository
    {
        private readonly AppDbContext _context;

        public FoodRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Food>> GetAllFoods()
        {
            var foods = _context.Foods.AsEnumerable();
            return Task.Run(() => foods);
        }
    }
}
