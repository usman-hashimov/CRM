using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Location.Queries;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Location.Handlers.Queries
{
    public class GetAllLocationsHandler : IRequestHandler<GetAllLocationsQuery, List<LocationModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllLocationsHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LocationModel>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _context.Locations.ToListAsync(cancellationToken);

            return locations;
        }
    }
}
