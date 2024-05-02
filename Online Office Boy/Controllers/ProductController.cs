using ApplicationService.Services.CategoryServices;
using ApplicationService.Services.ProductServices;
using ApplicationService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Office_Boy.Controllers
{
    [Authorize(Roles = "OfficeBoy")]
    public class ProductController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IWebHostEnvironment environment;
        private readonly ICategoryServices categoryServices;

        public ProductController(IProductServices productServices, IWebHostEnvironment environment, ICategoryServices categoryServices)
        {
            this.productServices = productServices;
            this.environment = environment;
            this.categoryServices = categoryServices;
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            ViewData["Categries"] = await categoryServices.GetAllAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductRequestVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await productServices.CreateAsync(model, environment.WebRootPath);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Image could not be saved successfully. Please try again.");
                    return View(model);
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

        public async Task<IActionResult> Index()
        {
            return View(await productServices.GetAllIncludingCategoryAsync());
        }
        [HttpGet]

        public async Task<IActionResult> Update(int productId)
        {
            ViewData["Categories"] = await categoryServices.GetAllAsync();
            var ss = await productServices.GetIncludinyCategoyAsync(productId);
            return View(ss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(ProductEditRequestVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Update", model);
                }

                var result = await productServices.UpdateAsync(model, environment.WebRootPath);

                if (result > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Image could not be saved successfully. Please try again.");
                    return RedirectToAction("Update", model);
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Product Id Is Invalid Please Select Exist Product.");
                    return RedirectToAction("Update", model);
                }
                else
                {
                    ModelState.AddModelError("", "Product could not be saved successfully Try Again.");
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
