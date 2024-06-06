using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;

namespace Snackis4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Snackis4User> _userManager;
        private readonly Snackis4Context _context;


        public IndexModel(UserManager<Snackis4User> userManager, Snackis4Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Snackis4User MyUser { get; set; }
        public List<Models.Category> Categories { get; set; }


        public async Task OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            Categories = await _context.Category.ToListAsync();
        }

    }
}
