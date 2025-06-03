using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.Services;
using ShopCRM.Application.UseCases.User.Commands;
using ShopCRM.Application.Utilities;
using ShopCRM.Application.Validators;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Handlers.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateUserHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user != null)
            {
                return new ResponseModel
                {
                    Message = "Such email already exists!",
                    Status = 409
                };
            }

            var validator = new UserValidator();

            var validatorResult = validator.Validate(request);

            if (validatorResult.IsValid)
            {
                string hashedPassword = PasswordHasher.HashPassword(request.Password);

                var userr = new UserModel
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    PasswordHash = hashedPassword,
                    Role = request.Role,
                    LocationId = request.LocationId,
                };

                if (request.Role == "SalesRep")
                {
                    var salesRep = new SalesRepModel
                    {
                        UserId = userr.Id
                    };

                    await _context.Users.AddAsync(userr);
                    await _context.SaveChangesAsync(cancellationToken: default);
                    
                    await _context.SalesReps.AddAsync(salesRep);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    await _context.Users.AddAsync(userr);
                    await _context.SaveChangesAsync(cancellationToken: default);
                }

                return new ResponseModel
                {
                    Message = "User added.",
                    Status = 200,
                    IsSuccess = true
                };
            }

            return new ResponseModel
            {
                Message = validatorResult.ToString(),
                Status = 403
            };
        }
    }
}
