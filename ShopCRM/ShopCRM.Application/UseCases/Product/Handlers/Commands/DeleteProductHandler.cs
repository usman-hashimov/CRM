using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Product.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Handlers.Commands
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public DeleteProductHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SaleItems)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                return new ResponseModel
                {
                    Message = "Product not found.",
                    Status = 404
                };
            }

            _context.Products.Remove(product);
            
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Product is updated.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
