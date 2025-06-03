using MediatR;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Category.Commands;
using ShopCRM.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCRM.Application.UseCases.Category.Handlers.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public CreateCategoryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new CategoryModel
            {
                Name = request.Name,
            };

            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Category created.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
