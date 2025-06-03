using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Commands
{
    public class CreateTaskCommand : IRequest<ResponseModel>
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
