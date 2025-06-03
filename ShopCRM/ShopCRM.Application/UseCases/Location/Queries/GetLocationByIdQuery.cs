using MediatR;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Location.Queries
{
    public class GetLocationByIdQuery : IRequest<LocationModel>
    {
        public Guid Id { get; set; }
        public string Term { get; set; } = "";
    }
}
