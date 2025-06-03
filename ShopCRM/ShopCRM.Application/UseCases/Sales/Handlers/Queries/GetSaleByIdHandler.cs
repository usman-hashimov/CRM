using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Sales.Queries;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Sales.Handlers.Queries
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleViewModel>
    {
        private readonly IAppDbContext _context;

        public GetSaleByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SaleViewModel> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .Include(s => s.SalesRep)
                    .ThenInclude(sr => sr.User)
                .Include(s => s.Location)
                .FirstOrDefaultAsync(s => s.Id == request.SaleId);

            if (sale == null)
            {
                return null;
            }

            var saleViewModel = new SaleViewModel
            {
                TotalAmount = sale.TotalAmount,
                SaleDate = sale.SaleDate,
                SalesRepName = sale.SalesRep.User.Name,
                SalesRepSurname = sale.SalesRep.User.Surname,
                Location = sale.Location.Address,
                SaleItems = sale.SaleItems.Select(si => new SaleItemViewModel
                {
                    Quantity = si.Quantity,
                    TotalAmount = si.TotalAmount,
                    ProductName = si.Product.Name
                }).ToList()
            };

            return saleViewModel;
        }
    }
}
