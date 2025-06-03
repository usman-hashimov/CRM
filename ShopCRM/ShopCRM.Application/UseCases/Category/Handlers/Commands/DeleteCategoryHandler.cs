using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ResponseModel>
    {
        private readonly IAppDbContext _context;

        public DeleteCategoryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (category == null)
            {
                return new ResponseModel
                {
                    Message = "Ctegory not found.",
                    Status = 404
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                Message = "Category deleted.",
                Status = 200,
                IsSuccess = true
            };
        }
    }
}
