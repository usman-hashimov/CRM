using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Location.Commands
{
    public class CreateLocationCommand : IRequest<ResponseModel>
    {
        public string Address { get; set; }
    }
}
