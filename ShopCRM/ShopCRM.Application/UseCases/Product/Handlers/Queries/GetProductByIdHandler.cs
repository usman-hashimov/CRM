using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Product.Queries;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Handlers.Queries
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly IAppDbContext _context;

        public GetProductByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SaleItems)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        
            if (product == null)
            {
                return null;
            }

            return product;
        }
    }
}
