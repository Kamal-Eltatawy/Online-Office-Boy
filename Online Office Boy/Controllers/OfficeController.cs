using ApplicationService.Services.CategoryServices;
using ApplicationService.Services.DepartmentServices;
using ApplicationService.Services.OrderProductServices;
using ApplicationService.Services.ProductServices;
using ApplicationService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Online_Office_Boy.Controllers
{
    [Authorize]
    public class OfficeController : Controller
    {
        private readonly IOfficeServices _officeServices;

        public OfficeController(IOfficeServices  officeServices)
        {
            this._officeServices = officeServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetOffices()
        {
            return Json(new {code=1, offices = await _officeServices.GetOfficesAsync()});
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Json(new { code = 1, departments = await _officeServices.GetAllAsync() });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Index()
        {
            return View(await _officeServices.GetAllAsync());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(OfficeCreateRequestVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await _officeServices.CreateAsync(model);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Product could not be saved successfully Try Again.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(model);
            }
        }
  
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int officeId)
        {
            return View(await _officeServices.GetAsync(officeId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(EditOfficeRequestVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Update", model);
                }

                var result = await _officeServices.UpdateAsync(model);

                if (result > 0)
                {
                    return RedirectToAction("Index", "Office");
                }
             
                else if (result == -5)
                {
                    ModelState.AddModelError("", "Office Id Is Invalid Please Select Exist Office.");
                    return RedirectToAction("Update", model);
                }
                else
                {
                    ModelState.AddModelError("", "Office could not be saved successfully Try Again.");
                    return RedirectToAction("Update", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return RedirectToAction("Update", model);
            }
        }

    }
}
