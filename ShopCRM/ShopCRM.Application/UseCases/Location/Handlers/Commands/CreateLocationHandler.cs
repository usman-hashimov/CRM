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
    public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateLocationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new LocationModel
            {
                Address = request.Address,
            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Location added.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
