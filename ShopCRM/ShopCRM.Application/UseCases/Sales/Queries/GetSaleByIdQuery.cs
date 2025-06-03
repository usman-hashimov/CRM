using MediatR;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleViewModel>
    {
        public Guid SaleId { get; set; }
    }
}
