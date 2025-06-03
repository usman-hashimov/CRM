using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.Services;
using ShopCRM.Application.UseCases.User.Commands;
using ShopCRM.Application.Utilities;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Handlers.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginUserHandler(IAppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ResponseModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "Email not found",
                    Status = 404
                };
            }

            string hashedPassword = PasswordHasher.HashPassword(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                return new ResponseModel
                {
                    Message = "Incorrect password",
                    Status = 403
                };
            }

            //string token = _tokenService.GenerateToken(user);

            return new ResponseModel
            {
                Message = "token",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
