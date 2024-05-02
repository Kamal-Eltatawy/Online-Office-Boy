using ApplicationService.Services.ProductServices;
using ApplicationService.Services.SiftsServices;
using ApplicationService.Services.UserServices;
using ApplicationService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Online_Office_Boy.Controllers
{
    [Authorize]
    public class ShiftController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IShiftServices shiftServices;

        public ShiftController(IUserServices userServices, IShiftServices shiftServices)
        {
            this.userServices = userServices;
            this.shiftServices = shiftServices;
        }
        [HttpGet]
        [Authorize("Permission.Shift.View")]
        public async Task<IActionResult> Index()
        {
            return View(await userServices.GetAllOfficeBoysIncludingShiftsAndOfficeAsync());
        }

        [HttpGet]
        [Authorize("Permission.Shift.Create")]
        public async Task<IActionResult> Create(string OfficeBoyId)
        {
            if (string.IsNullOrEmpty(OfficeBoyId))
            {
                return RedirectToAction("Index");
            }
            var officeBoy = await userServices.GetOfficeBoyByIdAsync(OfficeBoyId);
            if (officeBoy is not null)
            {
                return View(officeBoy);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Permission.Shift.Create")]
        public async Task<IActionResult> Create(OfficeBoyCreateShiftVM model)
        {
           
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await shiftServices.CreateOfficeBoyShiftAsync(model);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Office boy already has a shift assigned Update It.");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Shift update failed. Please try again.");
                    return View(model);
                }

            
     
        }

        [HttpGet]
        [Authorize("Permission.Shift.Update")]
        public async Task<IActionResult> Update(string OfficeBoyId)
        {

            if (string.IsNullOrEmpty(OfficeBoyId))
            {
                return RedirectToAction("Index");
            }
            var officeBoy = await userServices.GetOfficeBoyByIdIncludingShiftAsync(OfficeBoyId);
            if (officeBoy is not null)
            {
                return View(officeBoy);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize("Permission.Shift.Update")]
        public async Task<IActionResult> Update(OfficeBoyUpdateShiftVM model)
        {
            
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Update", model);
                }

                var result = await shiftServices.UpdatefficeBoyShiftsAsync(model);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Office doesn't have a shift assigned. Please assign one.");
                    return RedirectToAction("Update", model);
                }
                else
                {
                    ModelState.AddModelError("", "Shift update failed. Please try again.");
                    return RedirectToAction("Update", model);
                }
            }
           
        }

    }

