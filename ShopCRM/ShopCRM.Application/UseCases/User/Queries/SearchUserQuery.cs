using MediatR;
using ShopCRM.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.User.Queries
{
    public class SearchUserQuery : IRequest<List<UserViewModel>>
    {
        public string SearchTerm { get; set; }
    }
}
