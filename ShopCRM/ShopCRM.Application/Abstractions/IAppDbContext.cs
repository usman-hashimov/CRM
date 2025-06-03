using Microsoft.EntityFrameworkCore;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.Abstractions
{
    public interface IAppDbContext
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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
