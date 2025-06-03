using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.User.Queries;
using ShopCRM.Domain.Entities.Models;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Handlers.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllUsersHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<UserModel> query = _context.Users
                .Include(u => u.Location);

            if (!string.IsNullOrEmpty(request.Role) && request.Role.ToLower() != "all")
            {
                query = query.Where(u => u.Role.ToLower() == request.Role.ToLower());
            }

            var users = await query.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Role = u.Role,
                Location = u.Location.Address
            }).ToListAsync(cancellationToken);

            return users;
        }
    }
}
