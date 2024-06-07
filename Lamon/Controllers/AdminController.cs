using Microsoft.AspNetCore.Mvc;
using Lamon.Models;
using Lamon.Services;

namespace Lamon.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceBase<Category> categoryService;
        private readonly IServiceBase<Product> productService;
        private readonly IServiceBase<Order> orderService;
        public AdminController(IServiceBase<Category> categoryService,
            IServiceBase<Product> productService,
            IServiceBase<Order> orderService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.orderService = orderService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View(categoryService.GetAll().Where(c => c.IsDeleted == false).ToList());
        }

        
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);
            try
            {
                categoryService.Add(category);
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(category);
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            return View(categoryService.GetDetails(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(int id,Category category)
        {

            if (!ModelState.IsValid)
                return View(category);
            try
            {
                categoryService.Update(id,category);
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(category);
            }
            return View();

        }

        public IActionResult CategoryDetails(int id)
        {
            return View(categoryService.GetDetails(id));
        }

        public IActionResult DeleteCategory(int id)
        {
            categoryService.Delete(id);
            return RedirectToAction("Categories");
        }

        //products
        public IActionResult Products()
        {
            return View(productService.GetAll().Where(c => c.IsDeleted == false).ToList());
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = categoryService.GetAll().Where(c => c.IsDeleted == false).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            product.CreatedOn =DateTime.Now;
            ViewBag.Categories = categoryService.GetAll().Where(c => c.IsDeleted == false).ToList();

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //2 important things we must doing checking extention and size of file
                if(file != null)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        product.Image = dataStream.ToArray();
                    }
                }
            }

            if (product.Image == null)
            {
                ModelState.AddModelError("", "image of product Required");
                return View(product);
            }

            if (!ModelState.IsValid)
                return View(product);
            try
            {
                productService.Add(product);
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = categoryService.GetAll().Where(c => c.IsDeleted == false).ToList();
            return View(productService.GetDetails(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            ViewBag.Categories = categoryService.GetAll().Where(c => c.IsDeleted == false).ToList();
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //2 important things we must doing checking extention and size of file
                if (file != null)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        product.Image = dataStream.ToArray();
                    }
                }
            }
            else
            {
                product.Image = productService.GetDetails(id).Image;
            }

            if(product.Image == null)
            {
                ModelState.AddModelError("", "image of product Required");
                return View(product);
            }

            if (!ModelState.IsValid)
                return View(product);
            try
            {
                productService.Update(id, product);
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }
            return View();

        }

        public IActionResult ProductDetails(int id)
        {
            return View(productService.GetDetails(id));
        }

        public IActionResult DeleteProduct(int id)
        {
            productService.Delete(id);
            return RedirectToAction("Products");
        }

        public IActionResult Orders()
        {
            return View(orderService.GetAll());
        }

        public IActionResult DetailsOrder(int orderId)
        {
            Order order =  orderService.GetDetails(orderId);
            return View(order);
        }

        public IActionResult ConfirmOrder(int orderId)
        {
            Order order =new Order();
            order.StatusOfOrder = StatusOfOrder.ConfirmedStatus;
            orderService.Update(orderId, order);
            return RedirectToAction("Orders");
        }

        public IActionResult CancelOrder(int orderId)
        {
            Order order = new Order();
            orderService.Delete(orderId);
            return RedirectToAction("Orders");
        }
    }
}
