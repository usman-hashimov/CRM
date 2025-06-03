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
    public class SearchUserHandler : IRequestHandler<SearchUserQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context;

        public SearchUserHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserViewModel>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();

            var users = await _context.Users
                .Where(u => u.Name.ToLower().Contains(searchTerm) || u.Surname.ToLower().Contains(searchTerm))
                .Select(u => new UserViewModel
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
