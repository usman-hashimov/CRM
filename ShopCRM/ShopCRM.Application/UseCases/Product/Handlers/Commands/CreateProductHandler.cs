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
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateProductHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
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

            var product = new ProductModel
            {
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = category.Id,
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Product created.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
