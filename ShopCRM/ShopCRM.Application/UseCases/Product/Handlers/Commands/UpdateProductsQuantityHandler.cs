using MediatR;
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
    public class UpdateProductsQuantityHandler : IRequestHandler<UpdateProductsQuantityCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public UpdateProductsQuantityHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateProductsQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                return new ResponseModel
                {
                    Message = "Product not found.",
                    Status = 404
                };
            }

            if (request.ProductQuantity < 0)
            {
                return new ResponseModel
                {
                    Message = "Quantity is invalid, it's less than 0.",
                    Status = 400
                };
            }

            product.StockQuantity = request.ProductQuantity;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Product's quantity is updated.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
