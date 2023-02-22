using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public  async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();

                return View(result.Data);
          

        }





    }
}
