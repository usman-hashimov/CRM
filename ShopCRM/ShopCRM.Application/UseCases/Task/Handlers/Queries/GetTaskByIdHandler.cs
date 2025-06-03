using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Task.Queries;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Handlers.Queries
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskViewModel>
    {
        private readonly IAppDbContext _context;

        public GetTaskByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskViewModel> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == request.TaskId);

            if (task == null)
            {

            }

            var taskVmodel = new TaskViewModel
            {
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                UserName = task.User.Name,
                UserSurname = task.User.Surname
            };

            return taskVmodel;
        }
    }
}
