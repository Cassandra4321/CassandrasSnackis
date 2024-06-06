using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;

namespace Snackis4.Pages.Admin.CategoryAdmin
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
        public Models.Category Category { get; set; }

        public List<Models.Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);

            if (category != null)
            {
                Category = category;
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}