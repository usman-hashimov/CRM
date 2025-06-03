using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Commands
{
    public class UpdateTaskCommand : IRequest<ResponseModel>
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
