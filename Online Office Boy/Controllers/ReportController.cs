using ApplicationService.Services.OrderProductServices;
using ApplicationService.ViewModels;
using Domain.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace Online_Office_Boy.Controllers
{
    [Authorize(Roles ="OfficeBoy")]
    public class ReportController : Controller
    {
        private readonly IOrderProductServices orderProductServices;

        public ReportController(IOrderProductServices orderProductServices)
        {
            this.orderProductServices = orderProductServices;
        }
        public IActionResult Index ()
        {
            return View();
        }
   
        [HttpPost]
        public async Task<IActionResult> GetOrderHeaderTablePartial(ReportHeaderSearchRequestVM model)
        {
                return PartialView("_OrderReportHeader", await orderProductServices.GetReportOrdersHeaderSearch(model));
        }

        [HttpPost]
        public async Task<IActionResult> GetOrderDetailsTablePartial(ReportDetailsSearchRequestVM model)
        {
      
                return PartialView("_OrderReportDetails", await orderProductServices.GetReportOrdersDetailsSearch(model));
        }

        [HttpPost]
        public async Task<IActionResult> Print(ReportDetailsSearchRequestVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                    });
                }
                if (!string.IsNullOrEmpty(model.UserId) && model.SearchOptions == SearchOptions.Employee)
                {

                    return Json(await orderProductServices.PrintEmployeeReport(model));

                }
                else if (model.SearchOptions == SearchOptions.IsGuest)

                {
                    return Json(await orderProductServices.PrintGuestReport(model));

                }
                return Json(await orderProductServices.PrintAllAsync(model.OrderId, model.isPaid, model.From, model.To));
            }

            catch (Exception)
            {

                throw;
            }
        }
   

    }
}
