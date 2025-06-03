using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Application.UseCases.Product.Queries;
using ShopCRM.Application.UseCases.SaleItem.Commands;
using ShopCRM.Application.UseCases.Sales.Commands;
using ShopCRM.Application.UseCases.Sales.Queries;
using ShopCRM.Domain.Entities.ViewModels;

namespace ShopCRM.MVC.Controllers
{
    public class SaleController : Controller
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateSaleViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (model.SaleItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Add 1 or more items");

                return View(model);
            }
            else
            {
                var saleResponse = await _mediator.Send(new CreateSaleCommand
                {
                    SalesRepId = model.SalesRepId,
                    LocationId = model.LocationId
                });

                foreach (var saleItem in model.SaleItems)
                {
                    var saleItemResponse = await _mediator.Send(new CreateSaleItemCommand
                    {
                        SaleId = saleResponse,
                        ProductId = saleItem.ProductId,
                        Quantity = saleItem.Quantity
                    });

                    if (!saleItemResponse.IsSuccess)
                    {
                        ModelState.AddModelError(string.Empty, saleItemResponse.Message);
                        
                        return View(model);
                    }
                }

                return RedirectToAction("All", "Sales");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchProducts(string searchTerm)
        //{
        //    var query = new SearchProductQuery { SearchTerm = searchTerm };
        //    var products = await _mediator.Send(query);

        //    return Json(products);
        //}

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sales = await _mediator.Send(new GetAllSalesQuery());

            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var sale = await _mediator.Send(new GetSaleByIdQuery { SaleId = Id });

            return View(sale);
        }
    }
}
