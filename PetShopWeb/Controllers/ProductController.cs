using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data;
using PetShopWeb.Data.Entity;
using PetShopWeb.Models;

namespace PetShopWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _context;

        public ProductController(ILogger<HomeController> logger, ShopDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Product(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = category.Id;
            ViewBag.CategoryName = category.Name;

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
