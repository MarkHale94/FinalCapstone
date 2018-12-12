using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmaQueue.Models;

namespace PharmaQueue.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
             // Customize the ASP.NET Identity model and override the defaults if needed.
             // For example, you can rename the ASP.NET Identity table names and more.
             // Add your customizations after calling base.OnModelCreating(builder);
             modelBuilder.Entity<Prescription>()
                 .Property(b => b.DateCreated)
                 .HasDefaultValueSql("GETDATE()");

             ApplicationUser user = new ApplicationUser
             {
                 FirstName = "admin",
                 LastName = "admin",
                 UserName = "admin@admin.com",
                 NormalizedUserName = "ADMIN@ADMIN.COM",
                 Email = "admin@admin.com",
                 NormalizedEmail = "ADMIN@ADMIN.COM",
                 UserTypeId = 1,
                 EmailConfirmed = true,
                 LockoutEnabled = false,
                 SecurityStamp = Guid.NewGuid().ToString("D")
             };
             var passwordHash = new PasswordHasher<ApplicationUser>();
             user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
             modelBuilder.Entity<ApplicationUser>().HasData(user);

            ApplicationUser customer = new ApplicationUser
            {
                FirstName = "customer",
                LastName = "customer",
                UserName = "customer@customer.com",
                NormalizedUserName = "CUSTOMER@CUSTOMER.COM",
                Email = "CUSTOMER@CUSTOEMR.com",
                NormalizedEmail = "CUSTOMER@CUSTOMER.COM",
                UserTypeId = 2,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var newPasswordHash = new PasswordHasher<ApplicationUser>();
            customer.PasswordHash = newPasswordHash.HashPassword(customer, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(customer);

             modelBuilder.Entity<Status>().HasData(
                 new Status()
                 {
                     StatusId = 1,
                     QueueStatus = "Entered"
                 },
                 new Status()
                 {
                     StatusId = 2,
                     QueueStatus = "Reviewed"
                 },
                 new Status()
                 {
                     StatusId = 3,
                     QueueStatus = "Filled"
                 },
                 new Status()
                 {
                     StatusId = 4,
                     QueueStatus = "Ready"
                 }
             );

             modelBuilder.Entity<Prescription>().HasData(
                 new Prescription()
                 {
                     PrescriptionId = 1,
                     Name = "Metformin",
                     Strength = "500mg",
                     Quantity = 120,
                     Refills = 4,
                     IsSold = false,
                     Price = 5.00,
                     StatusId = 1,
                     UserId =customer.Id
                 },
                 new Prescription()
                 {
                     PrescriptionId = 2,
                     Name = "Glipizide",
                     Strength = "2.5mg",
                     Quantity = 30,
                     Refills = 12,
                     IsSold = false,
                     Price = 30.00,
                     StatusId = 1,
                     UserId = customer.Id
                 },
                 new Prescription()
                 {
                     PrescriptionId = 3,
                     Name = "Benzonatate",
                     Strength = "200mg",
                     Quantity = 20,
                     Refills = 0,
                     IsSold = false,
                     Price = 11.99,
                     StatusId = 2,
                     UserId = customer.Id
                 },
                 new Prescription()
                 {
                     PrescriptionId = 4,
                     Name = "Hydralazine",
                     Strength = "25mg",
                     Quantity = 30,
                     Refills = 2,
                     IsSold = false,
                     Price = 15.00,
                     StatusId = 4,
                     UserId = customer.Id
                 },
                 new Prescription()
                 {
                     PrescriptionId = 5,
                     Name = "Adderall",
                     Strength = "20mg",
                     Quantity = 60,
                     Refills = 0,
                     IsSold = false,
                     Price = 200.00,
                     StatusId = 3,
                     UserId = customer.Id
                 }
             );

             modelBuilder.Entity<UserType>().HasData(
                 new UserType()
                 {
                     UserTypeId = 1,
                     UserRole = "Employee"
                 }
             );

             modelBuilder.Entity<UserType>().HasData(
                 new UserType()
                 {
                     UserTypeId = 2,
                     UserRole = "Customer"
                 }
             );
        }
    }
}
