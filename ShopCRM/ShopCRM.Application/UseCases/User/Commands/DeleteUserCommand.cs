using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Commands
{
    public class DeleteUserCommand : IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
    }
}
