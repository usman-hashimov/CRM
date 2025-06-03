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
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateTaskHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "User not found.",
                    Status = 404
                };
            }

            var task = new TaskModel
            {
                Description = request.Description,
                AssignedTo = request.UserId
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Task created.",
                Status = 201,
                IsSuccess = true
            };
        }
    }
}
