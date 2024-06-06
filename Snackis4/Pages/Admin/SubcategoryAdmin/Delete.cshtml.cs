using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Admin.SubcategoryAdmin
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Snackis4Context _context;

        public DeleteModel(Snackis4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Subcategory Subcategory { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategory = await _context.Subcategory.Include(sc => sc.Category).SingleOrDefaultAsync(sc => sc.Id == id);

            if (subcategory == null)
            {
                return NotFound();
            }

            else
            {
                Subcategory = subcategory;
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategory.FindAsync(id);

            if (subcategory != null)
            {
                Subcategory = subcategory;
                _context.Subcategory.Remove(subcategory);
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}
