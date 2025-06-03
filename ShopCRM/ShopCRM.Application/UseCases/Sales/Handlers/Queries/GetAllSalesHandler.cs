using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Sales.Queries;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Sales.Handlers.Queries
{
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, List<SaleModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllSalesHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _context.Sales.ToListAsync(cancellationToken);

            return sales;
        }
    }
}
