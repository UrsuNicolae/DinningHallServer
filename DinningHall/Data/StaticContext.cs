using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;

namespace DinningHall.Data
{
    public static class StaticContext
    {
        public static ThreadSafeListWithLock<Order> Orders { get; set; } = new ThreadSafeListWithLock<Order>();

        public static ThreadSafeListWithLock<Table> Tables { get; set; } = new ThreadSafeListWithLock<Table>();

        public static ThreadSafeListWithLock<Waiter> Waiters { get; set; } = new ThreadSafeListWithLock<Waiter>();

        public static ThreadSafeListWithLock<Food> Foods { get; set; } = new ThreadSafeListWithLock<Food>();
    }
}
