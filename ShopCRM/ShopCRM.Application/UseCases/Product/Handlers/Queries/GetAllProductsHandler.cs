using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Product.Queries;
using ShopCRM.Domain.Entities.Models;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Handlers.Queries
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllProductsHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductModel> query = _context.Products
                .Include(p => p.Category);

            var products = await query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category.Name
            }).ToListAsync(cancellationToken);

            return products;
        }
    }
}
