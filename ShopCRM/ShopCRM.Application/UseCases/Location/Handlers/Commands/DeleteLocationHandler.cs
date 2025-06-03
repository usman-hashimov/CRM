using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Location.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Location.Handlers.Commands
{
    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public DeleteLocationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations
                .Include(l => l.Users)
                .Include(l => l.Sales)
                .FirstOrDefaultAsync(l => l.Id == request.Id);

            if (location == null)
            {
                return new ResponseModel
                {
                    Message = "Location not found.",
                    Status = 404
                };
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Location deleted.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
