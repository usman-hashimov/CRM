using MediatR;
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
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public UpdateTaskHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.TaskId, cancellationToken);

            if (task == null)
            {
                return new ResponseModel
                {
                    Message = "Task not found.",
                    Status = 404
                };
            }

            var user = await _context.Users.FindAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "User not found.",
                    Status = 404
                };
            }

            task.Description = request.Description;
            task.AssignedTo = request.UserId;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Task updated.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
