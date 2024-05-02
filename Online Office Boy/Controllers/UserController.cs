using AutoMapper;
using Domain.Entities;
using ApplicationService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ApplicationService.Services.UserServices;
using ApplicationService.Services.SiftsServices;
using Domain.Const;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Online_Office_Boy.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper mapper;
        private readonly IUserServices userServices;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IMapper mapper, IUserServices userServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.mapper = mapper;
            this.userServices = userServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetOfficeBoys(int officeId)
        {
            if (officeId == 0)
            {
                return Json(new { code = -1, Message = "Office Id Cant Be 0" });
            }
            var officeboys = await userServices.GetOfficeBoyWithCurrentShiftAsync(officeId);
            if (officeboys is not null)
            {
                return Json(new { code = 1, officeboys });

            }
            return Json(new { code = 0 });

        }

        [HttpGet]

        public async Task<IActionResult> GetOfficeBoysCurrentShiftOfficesAsync()
        {
            var offices = await userServices.GetOfficeBoysCurrentShiftOfficesAsync();
            if (offices is not null)
            {
                return Json(new { code = 1, offices });

            }
            return Json(new { code = 0 });

        }



        [HttpGet]
        [Authorize("Permission.User.View")]
        public async Task<IActionResult> Index()
        {
            return View(await userServices.GetAllUserIncludingOfficeAndRolesAsync());
        }

        [HttpGet]
        [Authorize("Permission.User.Update")]
        public async Task<IActionResult> UpdateRoles(string userId)
        {

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }
            var user = await userServices.GetUserIncludingAsync(userId);
            if (user is not null)
            {
                ViewData["Roles"] = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize("Permission.User.Update")]
        public async Task<IActionResult> UpdateRoles(UserWithRolesRequestVM model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("UpdateRoles", model);
            }

            var result = await userServices.AddUserToRoles(model);

            if (result.Code == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", $"{result.Message}");
                return RedirectToAction("UpdateRoles", model);
            }
        }


        [HttpGet]
        [Authorize(Roles = "OfficeBoy")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = await userServices.GetAllUserInSelectVMAsync();
            if (users is not null)
            {
                return Json(new { code = 1, users });

            }
            return Json(new { code = 0 });

        }



    }


}
