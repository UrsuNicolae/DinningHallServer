using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .HasOne(t => t.Order).WithOne(o => o.Table)
                .HasForeignKey<Order>(e => e.TableId);

            modelBuilder.Entity<Order>()
                .Property(o => o.FoodsIds)
                .HasConversion(
                    f => JsonConvert.SerializeObject(f),
                    f => JsonConvert.DeserializeObject<List<int>>(f));
        }
    }
}
