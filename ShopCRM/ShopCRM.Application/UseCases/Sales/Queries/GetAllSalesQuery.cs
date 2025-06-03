using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Sales.Queries
{
    public class GetAllSalesQuery : IRequest<List<SaleModel>>
    {
    }
}
