using ApplicationService.Services.DepartmentServices;
using ApplicationService.Services.OrderProductServices;
using ApplicationService.Services.OrderServices;
using ApplicationService.Services.ProductServices;
using ApplicationService.Services.UserServices;
using ApplicationService.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace Online_Office_Boy.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IOrderServices orderServices;
        private readonly IOrderProductServices orderProductServices;
        private readonly IProductServices productServices;

        public OrderController(IUserServices userServices, IOrderServices orderServices, IOrderProductServices orderProductServices, IProductServices productServices)
        {
            this.userServices = userServices;
            this.orderServices = orderServices;
            this.orderProductServices = orderProductServices;
            this.productServices = productServices;
        }
     
        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Shop()
        {
            return View(await productServices.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            return View(await orderProductServices.GetCurrentUserOrdersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { code = 0, message = errors });
            }

            var result = await orderServices.CreateAsync(model);
            if (result > 0)
            {
                return Json(new { code = 1 });
            }
            else if (result == -5)
            {
                return Json(new { code = -5 });

            }
            return Json(new { code = -1 });
        }


        [HttpGet]
        public async Task<IActionResult> GetCurrentUserOrders()
        {
            var orders = await orderProductServices.GetCurrentUserOrdersAsync();
            if (orders is not null)
            {
                return Json(new { code = 1, orders });

            }
            return Json(new { code = 0 });

        }


        [HttpPost]
        public async Task<IActionResult> CancelOrderProduct([FromBody] CancelOrderProductRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { code = 0, message = errors });
            }

            var result = await orderProductServices.CancelOrderProduct(model);
            if (result > 0)
            {
                return Json(new { code = 1 });
            }
            else if (result == -5 || result == -4)
            {
                return Json(new { code = result });

            }
            return Json(new { code = -1 });
        }


        //ToDO => DashBoard Area 



        [HttpGet]
        [Authorize(Roles = "OfficeBoy")]
        public IActionResult OrderStatusDashboard()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "OfficeBoy")]
        public async Task<IActionResult> OrderDashboardStatusPartialTable()
        {

            return PartialView("_OrderDashboardStatusPartialTable", await orderProductServices.GetAllOrdersForTheCurrentOfficeBoyThatWereNotDelivered());
        }


        [HttpGet]
        [Authorize(Roles = "OfficeBoy")]
        public IActionResult OrderIsPaidDashboard()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "OfficeBoy")]
        public async Task<IActionResult> OrderDashboardIsPaidPartialTable()
        {

            return PartialView("_OrderDashboardIsPaidPartialTable", await orderProductServices.GetAllOrdersForTheCurrentOfficeBoyThatWereNotPaid());
        }


        [HttpPost]
        [Authorize(Roles = "OfficeBoy")]


        public async Task<IActionResult> EditStatus(OrderEditStatusRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { code = 0, message = errors });
            }

            var result = await orderProductServices.EditStatus(model);
            if (result > 0)
            {
                return Json(new { code = 1 });
            }

            return Json(new { code = -1 });

        }


        [HttpPost]
        [Authorize(Roles = "OfficeBoy")]
        public async Task<IActionResult> EditIsPaid(OrderEditIsPaidRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { code = 0, message = errors });
            }

            var result = await orderProductServices.EditIsPaid(model);
            if (result > 0)
            {
                return Json(new { code = 1 });
            }

            return Json(new { code = -1 });

        }






    }
}
