using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Models
{
    public class CreateBuyerModel : PageModel
    {
        private readonly DomainContext? _context;

        [BindProperty]
        public Buyer? Buyer { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Buyers.Add(Buyer);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
