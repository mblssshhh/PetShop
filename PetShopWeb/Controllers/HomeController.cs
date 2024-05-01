using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopWeb.Data.Entity;
using PetShopWeb.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using PetShopWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace PetShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _context;

        public HomeController(ILogger<HomeController> logger, ShopDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View();
        }

        public IActionResult Registretion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Buyers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже зарегистрирован.");
                    return View("Registretion", model);
                }

                var user = new Buyer
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Phone = model.Phone,
                    Email = model.Email,
                };

                user.Password = HashPassword(model.Password);

                _context.Buyers.Add(user);
                await _context.SaveChangesAsync();

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }
            return View("Registretion", model);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginVerify(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Введите email и пароль.";
                return View("Login");
            }

            var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null && !VerifyPassword(user.Password, password))
            {
                ViewBag.ErrorMessage = "Пользователь с такими данными не существует.";
                return View("Login");
            }
            await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string hashedPassword, string password)
        {
            return hashedPassword == HashPassword(password);
        }
        private async Task Authenticate(Buyer user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
