using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Entity.Enums;

namespace WorkshopTaskApp.Repository.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "TVs" },
                new Category { Id = 2, Name = "LapTops" },
                new Category { Id = 3, Name = "Sound Systems" }
                );

            //Seed some products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Lab Top Dell", Description = "New laptop from Dell Ram 16, Core I7", Price = 32000, Quantity = 2, Discount = 0, CategoryId = 2 },
                new Product { Id = 2, Name = "TV Lg I5033233", Description = "4k tv with the Best Sale", Price = 10000, Quantity = 5, Discount = 2, CategoryId = 1 },
                new Product { Id = 3, Name = "Speaker Blue ", Description = "Speaker Blue Tooth Wireless -model GT 112 - Color Black", Price = 1000, Quantity = 20, Discount = 7, CategoryId = 3 },
                new Product { Id = 4, Name = "HP 15s-eq2009ne ", Description = ", Ryzen 7-5700u, 8GB, 512 SSD, 15.6", Price = 20000, Quantity = 7, Discount = 2, CategoryId = 2 },
                new Product { Id = 5, Name = "Speaker Blue2 ", Description = "Speaker Blue new Genereation Tooth Wireless -model GT 112 - Color Black", Price = 1250, Quantity = 20, Discount = 7, CategoryId = 3 },
                new Product { Id = 36, Name = "Speaker Blue 3", Description = "Speaker Blue Old Generation Tooth Wireless -model GT 112 - Color Black", Price = 750, Quantity = 20, Discount = 10, CategoryId = 3 }
                );


            //Seed user admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    Phone = "01120472668",
                    Email = "admin@gmail.com",
                    Password = "admin",
                    Address = "Cairo",
                    Role = RoleEnum.Admin,
                    CreatedAt = DateTime.Now
                });
        }
    }
}
