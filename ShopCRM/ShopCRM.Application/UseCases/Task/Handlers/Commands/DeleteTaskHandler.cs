using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Task.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Task.Handlers.Commands
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public DeleteTaskHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);

            if (task != null)
            {

            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Task deleted.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
