using MediatR;
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
    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public UpdateLocationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations.FindAsync(request.Id);

            if (location == null)
            {
                return new ResponseModel
                {
                    Message = "Location not found.",
                    Status = 404
                };
            }

            location.Address = request.Address;

            _context.Locations.Update(location);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Location updated.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
