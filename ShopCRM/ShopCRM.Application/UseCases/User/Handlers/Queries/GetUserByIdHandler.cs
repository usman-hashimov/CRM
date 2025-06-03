using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.User.Queries;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Handlers.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IAppDbContext _context;

        public GetUserByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Location)
                .Include(u => u.Interactions)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
            {
                return null;
            }

            if (user.Role == "SalesRep")
            {
                user = await _context.Users
                    .Include(u => u.SalesRep)
                        .ThenInclude(s => s.Sales)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);
            }

            return user;
        }
    }
}