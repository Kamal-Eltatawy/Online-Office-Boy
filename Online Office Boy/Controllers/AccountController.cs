using Domain.Entities;
using ApplicationService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApplicationService.Services.DepartmentServices;
using Domain.Const;

namespace Online_Office_Boy.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IOfficeServices _officeServices;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IOfficeServices officeServices)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this._officeServices = officeServices;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email OR Password Is Not Valid ");
                return View(userLogin);

            }

            var user = await userManager.FindByEmailAsync(userLogin.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email not found.");
                return View(userLogin);
            }

            var passwordIsValid = await userManager.CheckPasswordAsync(user, userLogin.Password);
            if (!passwordIsValid)
            {
                ModelState.AddModelError("Password", "Password is incorrect.");
                return View(userLogin);
            }

            await signInManager.SignInAsync(user, isPersistent: userLogin.KeppMe);
            #region Secound Way
            //var roles = await userManager.GetRolesAsync(user);

            //    if(roles.FirstOrDefault(i=>i.Equals(Roles.Admin.ToString(),StringComparison.OrdinalIgnoreCase))is not null)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            #endregion

            if (userManager.IsInRoleAsync(user, Roles.Admin.ToString()).Result || userManager.IsInRoleAsync(user, Roles.OfficeBoy.ToString()).Result)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("UserIndex", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewData["Offices"] = await _officeServices.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModelRequest userViewModelRequest)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email or Password is not valid.");
                return View(userViewModelRequest);
            }

            var existingEmailUser = await userManager.FindByEmailAsync(userViewModelRequest.Email);
            if (existingEmailUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(userViewModelRequest);
            }

            var existingUserNameUser = await userManager.FindByNameAsync(userViewModelRequest.UserName);
            if (existingUserNameUser != null)
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                return View(userViewModelRequest);
            }

            var user = mapper.Map<User>(userViewModelRequest);

            var result = await userManager.CreateAsync(user, userViewModelRequest.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userViewModelRequest);
            }

            return RedirectToAction("Login", "Account");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
