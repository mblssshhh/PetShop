using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Models;
using System.Diagnostics;
using Domain;
using Domain.Entities;

namespace PetShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DomainContext _context;

        public HomeController(ILogger<HomeController> logger, DomainContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
         
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Account()
        {
            var model = new CreateBuyerModel()
            {
                Buyer = new Buyer()
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
