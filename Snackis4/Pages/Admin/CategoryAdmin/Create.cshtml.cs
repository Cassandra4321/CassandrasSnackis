using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;

namespace Snackis4.Pages.Admin.CategoryAdmin
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
        public Models.Category Category { get; set; }

        public List<Models.Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Category.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Category.ToListAsync();
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}