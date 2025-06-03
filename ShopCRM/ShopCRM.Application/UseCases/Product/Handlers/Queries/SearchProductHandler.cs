using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Product.Queries;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Handlers.Queries
{
    public class SearchProductHandler : IRequestHandler<SearchProductQuery, List<ProductViewModel>>
    {
        private readonly IAppDbContext _context;

        public SearchProductHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(request.SearchTerm))
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Category = p.Category.Name
                })
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
