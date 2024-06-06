using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Admin.SubcategoryAdmin
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Snackis4Context _context;

        public CreateModel(Snackis4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Subcategory Subcategory { get; set; }
        public List<Category> Categories { get; set; }
        public List<Subcategory> Subcategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Category.ToListAsync();
            Subcategories = await _context.Subcategory.Include(s => s.Category).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                Categories = await _context.Category.ToListAsync();
                Subcategories = await _context.Subcategory.Include(s => s.Category).ToListAsync();
                return Page();
            }
            _context.Subcategory.Add(Subcategory);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
