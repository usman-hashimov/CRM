using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopCRM.Application.UseCases.Category.Queries;
using ShopCRM.Application.UseCases.Product.Commands;
using ShopCRM.Application.UseCases.Product.Queries;
using ShopCRM.Domain.Entities.ViewModels;
using System.Threading;

namespace ShopCRM.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Product");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, res.Message);
                }
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { ProductId = Id });

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { ProductId= Id });

            var command = new UpdateProductCommand
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Product");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, res.Message);
                }
            }

            return View(command);
        }
    }
}