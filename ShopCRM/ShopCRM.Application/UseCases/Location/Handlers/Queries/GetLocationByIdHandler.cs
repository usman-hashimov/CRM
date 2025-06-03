using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Location.Queries;
using ShopCRM.Domain.Entities.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Location.Handlers.Queries
{
    public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, LocationModel>
    {
        private readonly IAppDbContext _context;

        public GetLocationByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<LocationModel> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<LocationModel> query = _context.Locations
                .Include(l => l.Users)
                .Include(l => l.Sales);

            //if (request.Term == "Users")
            //{
            //    query = query.Include(l => l.Users);
            //}
            //else if (request.Term == "Sales")
            //{
            //    query = query.Include(l => l.Sales);
            //}


            var location = await query.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            return location;
        }
    }
}
