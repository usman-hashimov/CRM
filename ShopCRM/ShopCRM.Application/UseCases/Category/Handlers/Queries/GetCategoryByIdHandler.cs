using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Category.Queries;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Category.Handlers.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryModel>
    {
        private readonly IAppDbContext _context;

        public GetCategoryByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (category == null)
            {
                return null;
            }

            return category;
        }
    }
}
