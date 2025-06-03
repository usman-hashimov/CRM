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
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public UpdateCategoryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id, cancellationToken);

            if (category == null)
            {
                return new ResponseModel
                {
                    Message = "Category not found.",
                    Status = 404
                };
            }

            category.Name = request.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Category updated.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
