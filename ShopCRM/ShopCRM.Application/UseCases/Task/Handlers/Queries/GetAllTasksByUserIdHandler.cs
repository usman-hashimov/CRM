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
    public class GetAllTasksByUserIdHandler : IRequestHandler<GetAllTasksByUserIdQuery, List<TaskViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllTasksByUserIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskViewModel>> Handle(GetAllTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId, cancellationToken);

            if (user == null)
            {

            }

            var tasks = await _context.Tasks
                .Where(t => t.AssignedTo == request.UserId)
                .Select(t => new TaskViewModel
                {
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    UserName = user.Name,
                    UserSurname = user.Surname,
                }).ToListAsync(cancellationToken);

            return tasks;
        }
    }
}
