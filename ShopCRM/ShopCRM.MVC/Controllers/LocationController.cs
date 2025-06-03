using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Application.Abstractions;
using ShopCRM.Application.UseCases.Location.Commands;
using ShopCRM.Application.UseCases.Location.Queries;

namespace ShopCRM.MVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationCommand command)
        {
            if (ModelState.IsValid)
            {

                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Location");
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
            var locations = await _mediator.Send(new GetAllLocationsQuery());

            return View(locations);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid locationId)
        {
            var location = await _mediator.Send(new GetLocationByIdQuery { Id = locationId });

            if (location == null)
            {
                return NotFound();
            }

            var command = new UpdateLocationCommand
            {
                Id = location.Id,
                Address = location.Address
            };

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateLocationCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);

                if (res.IsSuccess)
                {
                    return RedirectToAction("All", "Location");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, res.Message);
                }
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> LocationDetails(Guid locationId)
        {
            var location = await _mediator.Send(new GetLocationByIdQuery { Id=locationId });

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLocationCommand { Id = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return RedirectToAction("All", "Location");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return View("Update", new UpdateLocationCommand { Id = id });
        }

    }
}
