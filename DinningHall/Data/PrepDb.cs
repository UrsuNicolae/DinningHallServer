using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Models;
using DinningHall.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DinningHall.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Foods.Any())
            {
                Console.WriteLine("--> Seeding foods...");

                context.Foods.AddRange(
                    new List<Food>{
        new()
        {
            Id = 1,
            Name = "Pizza",
            PreparationTime = 20,
            Complexity = 2,
            CookingApparatus = CookingApparatuses.Oven
        },
        new ()
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = 10,
            Complexity = 1,
            CookingApparatus = CookingApparatuses.None
        },
        new ()
        {
            Id = 3,
            Name = "Zeama",
            PreparationTime = 7,
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Stove
        },
        new ()
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Cofit",
            PreparationTime = 32,
            Complexity = 3,
            CookingApparatus = CookingApparatuses.None
        },
        new()
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35,
            Complexity = 3,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = 10,
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Stove
        },
        new()
        {
            Id = 7,
            Name = "Aubergine",
            PreparationTime = 20,
            Complexity = 2,
            CookingApparatus = CookingApparatuses.None
        },
        new()
        {
            Id = 8,
            Name = "Lasagna",
            PreparationTime = 30,
            Complexity = 2,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 9,
            Name = "Burger",
            PreparationTime = 15,
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 10,
            Name = "Gyros",
            PreparationTime = 15,
            Complexity = 1,
            CookingApparatus = CookingApparatuses.None
        }
        }
                );
            }
            else
            {
                Console.WriteLine("--> We already have foods");
            }

            if (!context.Waiters.Any())
            {
                Console.WriteLine("--> Seeding waiters...");

                context.Waiters.AddRange(new List<Waiter>
                {
                    new ()
                    {
                        IsFree = true
                    },
                    new ()
                    {
                        IsFree = true
                    },
                    new ()
                    {
                        IsFree = true
                    }
                });
            }
            else
            {
                Console.WriteLine("--> We already have waiters");
            }

            if (!context.Tables.Any())
            {
                Console.WriteLine("--> Seeding tables...");
                context.Tables.AddRange(
                    new List<Table>
                {
                        new ()
                        {
                            IsFree = true,
                            Order = null,
                            TableStatus = TableStatus.WaitToOrder
                        },
                        new ()
                        {
                            IsFree = true,
                            Order = null,
                            TableStatus = TableStatus.WaitToOrder
                        },
                        new ()
                        {
                            IsFree = true,
                            Order = null,
                            TableStatus = TableStatus.WaitToOrder
                        },
                        new ()
                        {
                            IsFree = true,
                            Order = null,
                            TableStatus = TableStatus.WaitToOrder
                        },
                        new ()
                        {
                            IsFree = true,
                            Order = null,
                            TableStatus = TableStatus.WaitToOrder
                        }
                });
            }
            else
            {
                Console.WriteLine("--> We already have tables");
            }

            context.SaveChanges();
        }
    }
}
