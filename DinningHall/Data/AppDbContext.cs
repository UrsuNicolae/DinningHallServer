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
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Waiter> Waiters { get; set; }

        public DbSet<Food> Foods { get; set; }
    }
}
