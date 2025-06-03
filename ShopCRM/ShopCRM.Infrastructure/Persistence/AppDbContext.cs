using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Domain.Entities.Models;
using System;

namespace ShopCRM.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SalesRepModel> SalesReps { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<SaleItemModel> SaleItems { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<InteractionModel> Interactions { get; set; }
        public DbSet<LocationModel> Locations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User and Interaction relationship with cascade delete
            modelBuilder.Entity<InteractionModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interactions)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User and SalesRep relationship with cascade delete
            modelBuilder.Entity<SalesRepModel>()
                .HasOne(sr => sr.User)
                .WithOne(u => u.SalesRep)
                .HasForeignKey<SalesRepModel>(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User and Task relationship with cascade delete
            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Cascade);

            // User and Location relationship
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Location)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LocationId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletion of users if a location is deleted

            // Product and Category relationship with cascade delete
            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sale and SaleItems relationship with cascade delete
            modelBuilder.Entity<SaleModel>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sale and SalesRep relationship with cascade delete
            modelBuilder.Entity<SaleModel>()
                .HasOne(s => s.SalesRep)
                .WithMany(sr => sr.Sales)
                .HasForeignKey(s => s.SalesRepId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sale and Location relationship
            modelBuilder.Entity<SaleModel>()
                .HasOne(s => s.Location)
                .WithMany(l => l.Sales)
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading deletion of sales if a location is deleted

            // SaleItem and Product relationship with cascade delete
            modelBuilder.Entity<SaleItemModel>()
                .HasOne(si => si.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
