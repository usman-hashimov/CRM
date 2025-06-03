using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Application.UseCases.Location.Queries;
using ShopCRM.Application.UseCases.User.Commands;
using ShopCRM.Application.UseCases.User.Queries;
using ShopCRM.Domain.Entities.ViewModels;

namespace ShopCRM.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers(string role = "all", string search = "")
        {
            var query = new SearchUserQuery
            {
                SearchTerm = search
            };

            var users = await _mediator.Send(query);

            if (role != "all")
            {
                users = users.Where(u => u.Role.ToLower() == role.ToLower()).ToList();
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserCommand createUserCommand)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(createUserCommand);

                if (result.IsSuccess)
                {
                    return RedirectToAction("AllUsers", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View(createUserCommand);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { UserId = Id });

            return View(user);
        }
    }
}
