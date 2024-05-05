using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data;
using PetShopWeb.Data.Entity;
using PetShopWeb.Models;
using System.Text;

namespace PetShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _context;

        public AccountController(ILogger<HomeController> logger, ShopDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Account()
        {
            var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(User.Identity.Name));

            var model = new UserProfileViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Phone = user.Phone,
                Email = user.Email,
                Money = user.Money
            };

            var userBasket = await _context.Buyers
            .Include(b => b.Buskets)
            .ThenInclude(b => b.Orders) 
            .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userBasket != null)
            {
                var orders = userBasket.Buskets
                    .SelectMany(b => b.Orders) 
                    .ToList();
                ViewBag.Orders = orders;
            }

            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(User.Identity.Name));

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Patronymic = model.Patronymic;
                    user.Phone = model.Phone;
                    user.Email = model.Email;

                    _context.Buyers.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Account", "Account");
            }
            return View("Account", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddMoney(decimal moneyToAdd)
        {
            var buyerId = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(User.Identity.Name));
            var buyer = await _context.Buyers.FindAsync(buyerId.Id);

            if (buyer == null)
            {
                return NotFound();
            }

            buyer.Money += moneyToAdd;
            await _context.SaveChangesAsync();

            return RedirectToAction("Account", "Account");
        }
    }
}
