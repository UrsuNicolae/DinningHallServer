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
                Console.WriteLine("--> Seeding data...");

                context.Foods.AddRange(
                    new List<Food>{
        new()
        {
            Id = 1,
            Name = "Pizza",
            PreparationTime = TimeSpan.FromSeconds(20),
            Complexity = 2,
            CookingApparatus = CookingApparatuses.Oven
        },
        new ()
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = TimeSpan.FromSeconds(10),
            Complexity = 1,
            CookingApparatus = CookingApparatuses.None
        },
        new ()
        {
            Id = 3,
            Name = "Zeama",
            PreparationTime = TimeSpan.FromSeconds(7),
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Stove
        },
        new ()
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Cofit",
            PreparationTime = TimeSpan.FromSeconds(32),
            Complexity = 3,
            CookingApparatus = CookingApparatuses.None
        },
        new()
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = TimeSpan.FromSeconds(35),
            Complexity = 3,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = TimeSpan.FromSeconds(10),
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Stove
        },
        new()
        {
            Id = 7,
            Name = "Aubergine",
            PreparationTime = TimeSpan.FromSeconds(20),
            Complexity = 2,
            CookingApparatus = CookingApparatuses.None
        },
        new()
        {
            Id = 8,
            Name = "Lasagna",
            PreparationTime = TimeSpan.FromSeconds(30),
            Complexity = 2,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 9,
            Name = "Burger",
            PreparationTime = TimeSpan.FromSeconds(15),
            Complexity = 1,
            CookingApparatus = CookingApparatuses.Oven
        },
        new()
        {
            Id = 10,
            Name = "Gyros",
            PreparationTime = TimeSpan.FromSeconds(15),
            Complexity = 1,
            CookingApparatus = CookingApparatuses.None
        }
        }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
