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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public UpdateProductHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            if (request.StockQuantity < 0)
            {
                return new ResponseModel
                {
                    Message = "Quantity is invalid, it's less than 0.",
                    Status = 400
                };
            }

            var category = await _context.Categories.FindAsync(request.CategoryId, cancellationToken);

            if (category == null)
            {
                return new ResponseModel
                {
                    Message = "Such category not found.",
                    Status = 404
                };
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.CategoryId = request.CategoryId;

            _context.Products.Update(product);
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
