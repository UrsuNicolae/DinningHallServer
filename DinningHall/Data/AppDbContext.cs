using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;
using Microsoft.EntityFrameworkCore;

namespace DinningHall.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
            
        }
        
        private DbSet<Order> Orders { get; set; }
    }
}
