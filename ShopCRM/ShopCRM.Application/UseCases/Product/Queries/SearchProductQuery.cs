using MediatR;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Product.Queries
{
    public class SearchProductQuery : IRequest<List<ProductViewModel>>
    {
        public string SearchTerm { get; set; }
    }
}
