using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.User.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Handlers.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public DeleteUserHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .Include(u => u.Interactions)
                .Include(u => u.Location)
                .Include(u => u.SalesRep)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "User not found.",
                    Status = 404
                };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "User deleted.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
