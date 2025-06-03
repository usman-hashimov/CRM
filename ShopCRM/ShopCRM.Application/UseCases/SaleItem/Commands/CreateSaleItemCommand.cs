using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.SaleItem.Commands
{
    public class CreateSaleItemCommand : IRequest<ResponseModel>
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
    }
}
