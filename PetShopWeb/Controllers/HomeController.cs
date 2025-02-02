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
using System.Security.Cryptography;

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

        public string GenerateOrderNumber()
        {
            string orderNumber = "#";
            Random random = new Random();
            bool isUnique = false;

            while (!isUnique)
            {
                for (int i = 0; i < 6; i++)
                {
                    orderNumber += random.Next(10).ToString();
                }

                if (!_context.Orders.Any(o => o.Number == orderNumber))
                {
                    isUnique = true;
                }
                else
                {
                    orderNumber = "#";
                }
            }

            return orderNumber;
        }


        public async Task<IActionResult> CheckoutAsync(decimal totalCost, int[] itemCounts)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            var user = await _context.Buyers
                                        .Include(u => u.Buskets)
                                        .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            if (totalCost == 0)
            {
                return RedirectToAction("Basket", "Home");
            }

            int count = 0;

            for (int i = 0; i < itemCounts.Length; i++)
            {
                count += itemCounts[i];
                foreach (var basket in user.Buskets) 
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basket.ProductId);
                    if (product != null)
                    {
                        product.Count -= itemCounts[i];
                        _context.Products.Update(product);
                    }
                }
            }
            await _context.SaveChangesAsync();



            if (user.Money >= totalCost)
            {
                user.Money -= totalCost;

                var order = new Order
                {
                    BusketId = user.Buskets.FirstOrDefault().Id,
                    Price = totalCost,
                    Date = DateTime.Now,
                    Amount = count,
                    Number = GenerateOrderNumber()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var basket in user.Buskets)
                {
                    basket.Status = "��������";
                }
                await _context.SaveChangesAsync();

                TempData["Busket"] = "����� ������� ������";
                return RedirectToAction("Basket", "Home");
            }
            else
            {
                TempData["ErrorBusket"] = "������������ ������� ��� ������ ������";
                return RedirectToAction("Basket", "Home");
            }
        }


        public IActionResult DeleteFromBasket(int itemId)
        {
            var itemToDelete = _context.Buskets.FirstOrDefault(i => i.Id == itemId);

            if (itemToDelete == null)
            {
                return NotFound(); 
            }

            _context.Buskets.Remove(itemToDelete);
            _context.SaveChanges();

            return RedirectToAction("Basket");
        }
        public async Task<IActionResult> BasketAsync()
        {
            var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(User.Identity.Name));

            if (user == null)
            {
                return NotFound();
            }

            var userBasket = await _context.Buyers
                .Include(b => b.Buskets)
                    .ThenInclude(b => b.Product)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userBasket == null)
            {
                return NotFound();
            }

            var basketModels = userBasket.Buskets
                .Where(b => !string.Equals(b.Status.Trim(), "��������", StringComparison.OrdinalIgnoreCase))
                .Select(b => new BusketModel
                {
                    Id = b.Id,
                    BuyerId = b.BuyerId,
                    ProductId = b.ProductId,
                    Count = b.Count,
                    Product = b.Product
                })
                .ToList();

            return View(basketModels);
        }


        public async Task<IActionResult> AddToBasketAsync(int productId, int count)
        {
            var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(User.Identity.Name));
            var existingItem = _context.Buskets.FirstOrDefault(b => b.BuyerId == user.Id && b.ProductId == productId && b.Status == "�� ��������");

            if (existingItem != null)
            {
                existingItem.Count += count;
            }
            else
            {
                var basketItem = new Busket
                {
                    BuyerId = user.Id,
                    ProductId = productId,
                    Count = count,
                    Status = "�� ��������"
                };
                _context.Buskets.Add(basketItem);
            }

            _context.SaveChanges();

            TempData["Message"] = "����� ������� �������� � �������!";
            return RedirectToAction("Catalog");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string query)
        {
            var products = _context.Products
                .Where(p => p.Name.Contains(query))
                .ToList();

            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "���" });

            ViewBag.Categories = categories;

            List<ProductModel> productModels = products.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Count = p.Count,
                Price = p.Price,
                Weight = p.Weight,
                Manufacturer = p.Manufacturer,
                Description = p.Description,
                ImagePath = p.ImagePath,
                CategoryId = p.CategoryId
            }).ToList();

            return View("Catalog", productModels);
        }

        public IActionResult Catalog(int? categoryId)
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            categories.Insert(0, new Category { Id = 0, Name = "���" });

            ViewBag.Categories = categories;
            ViewBag.CategoryId = categoryId; 

            List<ProductModel> productModels = products.Where(p => p.Count > 0).Select(p => new ProductModel
            {
                Id= p.Id,
                Name = p.Name,
                Count = p.Count,
                Price = p.Price,
                Weight = p.Weight,
                Manufacturer = p.Manufacturer,
                Description = p.Description,
                ImagePath = p.ImagePath,
                CategoryId = p.CategoryId 
            }).ToList();

            if (categoryId.HasValue && categoryId != 0)
            {
                productModels = productModels.Where(p => p.CategoryId == categoryId).ToList();
            }

            return View(productModels);
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
                    ModelState.AddModelError("Email", "������������ � ����� email ��� ���������������.");
                    return View("Registretion", model);
                }

                var user = new Buyer
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Phone = model.Phone,
                    Email = model.Email,
                    Money = 0,
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
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
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
                ViewBag.ErrorMessage = "������� email � ������.";
                return View("Login");
            }

            var user = await _context.Buyers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                ViewBag.ErrorMessage = "������������ � ������ ������� �� ����������.";
                return View("Login");
            }

            if (!VerifyPassword(user.Password, password))
            {
                ViewBag.ErrorMessage = "�������� ������.";
                return View("Login");
            }

            await Authenticate(user);

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string hashedPasswordFromDatabase, string inputPassword)
        {
            string trimmedHashedPassword = hashedPasswordFromDatabase.Trim();
            string trimmedInputPassword = inputPassword.Trim();

            return string.Equals(trimmedHashedPassword, HashPassword(trimmedInputPassword), StringComparison.Ordinal);
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
