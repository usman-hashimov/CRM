using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.SaleItem.Queries;
using ShopCRM.Domain.Entities.Models;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.SaleItem.Handlers.Queries
{
    public class GetAllSaleItemsHandler : IRequestHandler<GetAllSaleItemsQuery, List<SaleItemViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllSaleItemsHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleItemViewModel>> Handle(GetAllSaleItemsQuery request, CancellationToken cancellationToken)
        {
            var saleItems = await _context.SaleItems
                .Include(s => s.Product)
                .Select(s => new SaleItemViewModel
                {
                    Quantity = s.Quantity,
                    TotalAmount = s.TotalAmount,
                    ProductName = s.Product.Name
                })
                .ToListAsync();

            return saleItems;
        }
    }
}
