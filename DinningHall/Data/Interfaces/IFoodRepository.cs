using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;

namespace DinningHall.Data.Interfaces
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAllFoods();
    }
}
