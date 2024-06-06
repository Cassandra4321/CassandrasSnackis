using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Forum
{
    public class SubcategoriesModel : PageModel
    {

        private readonly Snackis4Context _context;

        public SubcategoriesModel(Snackis4Context context)
        {
            _context = context;
        }
        public Category Category { get; set; }
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

        public async Task<IActionResult> OnGetAsync(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (Category == null)
            {
                return NotFound();
            }

            Subcategories = await _context.Subcategory.Where(s => s.CategoryId == Category.Id).ToListAsync();
            return Page();
        }
    }
}
