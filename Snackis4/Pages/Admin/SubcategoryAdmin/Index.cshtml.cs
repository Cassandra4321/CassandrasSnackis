using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Admin.SubcategoryAdmin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Snackis4Context _context;

        public IndexModel(Snackis4Context context)
        {
            _context = context;
        }

        public List<Subcategory> Subcategories { get; set; }

        public async Task OnGetAsync()
        {
            Subcategories = await _context.Subcategory
                .Include(s => s.Category) 
                .ToListAsync();
        }
    }
}
