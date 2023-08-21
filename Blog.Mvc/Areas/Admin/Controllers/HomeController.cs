using Blog.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}