
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using GreenAgriHub.Models;


namespace GreenAgriHub.Models
{
    // DB Context class for the application
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public string DbPath { get; private set; }

        //A constructor that takes a DbContextOptions object and passes it to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Farmer>()
                .HasMany(f => f.Products)
                .WithOne(p => p.Farmer)
                .HasForeignKey(p => p.FarmerId);

            //Seeding Employee data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    EmployeeName = "Jack",
                    EmployeePassword = "jack10",
                    EmployeeEmail = "jack@gmail.com"
                },
                new Employee
                {
                    EmployeeId = 2,
                    EmployeeName = "Bob",
                    EmployeePassword = "bob20",
                    EmployeeEmail = "bob@gmail.com"
                }


            );
            //Seeding Farmer data
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer
                {
                    FarmerId = 1,
                    FarmerName = "Thabo",
                    FarmerSurname = "Nkosi",
                    FarmerPhone = "0612345678",
                    FarmerEmail = "tnkosi@gmail.com",
                    FarmerPassword = "nkosi123"
                },
                new Farmer
                {
                    FarmerId = 2,
                    FarmerName = "Nathi",
                    FarmerSurname = "Zondo",
                    FarmerPhone = "0712345678",
                    FarmerEmail = "nzondo@gmail.com",
                    FarmerPassword = "zondo123"
                }

            );
        }


    }

}
