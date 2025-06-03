using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Application.UseCases.Category.Commands;
using ShopCRM.Application.UseCases.Category.Queries;

namespace ShopCRM.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Category");
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
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id = Id });

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id= Id });

            if (category == null)
            {
                return NotFound();
            }

            var command = new UpdateCategoryCommand()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Category");
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
