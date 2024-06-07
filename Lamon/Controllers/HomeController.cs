using Microsoft.AspNetCore.Mvc;
using Lamon.Models;
using Lamon.Services;
using System.Diagnostics;

namespace Lamon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceBase<Category> categoryService;
        private readonly IServiceBase<Product> productService;



        public HomeController(ILogger<HomeController> logger,
            IServiceBase<Category> categoryService,
            IServiceBase<Product> productService)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            List<string> Namescategories = new List<string>();
            foreach (var category in categoryService.GetAll())
            {
                Namescategories.Add(category.Name);
            }
            ViewBag.NamesCategories = Namescategories;
            return View(productService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}