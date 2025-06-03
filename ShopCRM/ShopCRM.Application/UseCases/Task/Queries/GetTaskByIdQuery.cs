using MediatR;
using ShopCRM.Domain.Entities.Models;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskViewModel>
    {
        public Guid TaskId { get; set; }
    }
}
