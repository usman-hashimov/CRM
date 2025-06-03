using MediatR;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.SaleItem.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.SaleItem.Handlers.Commands
{
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateSaleItemHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
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

            var sale = await _context.Sales.FindAsync(request.SaleId, cancellationToken);

            if (sale == null)
            {
                return new ResponseModel
                {
                    Message = "Sale not found.",
                    Status = 404
                };
            }

            var saleItem = new SaleItemModel
            {
                Quantity = request.Quantity,
                TotalAmount = request.Quantity*product.Price,
                ProductId = request.ProductId,
                SaleId = request.SaleId,
            };

            sale.TotalAmount += saleItem.TotalAmount;

            var dif = product.StockQuantity - saleItem.Quantity;

            if (dif < 0)
            {
                return new ResponseModel
                {
                    Message = "Purchasing quantity more than stock quantity of product.",
                    Status = 400
                };
            }
            else
            {
                product.StockQuantity -= saleItem.Quantity;
            }

            await _context.SaleItems.AddAsync(saleItem);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Sale item created.",
                Status = 201,
                IsSuccess = true
            };
        }
    }
}
