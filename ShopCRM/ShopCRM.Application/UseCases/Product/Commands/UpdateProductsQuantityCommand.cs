using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Commands
{
    public class UpdateProductsQuantityCommand : IRequest<ResponseModel>
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
