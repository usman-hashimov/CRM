using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Task.Queries;
using ShopCRM.Domain.Entities.Models;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Handlers.Queries
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, List<TaskViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllTasksHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskViewModel>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            IQueryable<TaskModel> query = _context.Tasks
                .Include(t => t.User);

            if (request.Term != "all")
            {
                query = query.Where(t => t.Status.ToLower() == request.Term.ToLower());
            }

            var tasks = await query.Select(t => new TaskViewModel
            {
                Description = t.Description,
                DueDate = t.DueDate,
                Status = t.Status,
                UserName = t.User.Name,
                UserSurname = t.User.Surname
            }).ToListAsync(cancellationToken);

            return tasks;
        }
    }
}
