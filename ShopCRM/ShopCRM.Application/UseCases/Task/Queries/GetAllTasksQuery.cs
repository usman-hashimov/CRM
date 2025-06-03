using MediatR;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Queries
{
    public class GetAllTasksQuery : IRequest<List<TaskViewModel>>
    {
        public string Term { get; set; } = "all";
    }
}
