using MediatR;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Sales.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Sales.Handlers.Commands
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateSaleHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new SaleModel
            {
                LocationId = request.LocationId,
                SalesRepId = request.SalesRepId,
            };

            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return sale.Id;
        }
    }
}
