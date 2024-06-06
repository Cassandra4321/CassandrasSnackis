using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Forum
{
    public class ShowPostsModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public ShowPostsModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public List<ViewPost> Posts { get; set; } = new List<ViewPost>();

        //[BindProperty]
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }


        public async Task<IActionResult> OnGetAsync(int subcategoryId)
        {
            SubcategoryId = subcategoryId;
            Subcategory = await _context.Subcategory.FirstOrDefaultAsync(s => s.Id == SubcategoryId);

            if (Subcategory == null)
            {
                return NotFound();
            }

            var posts = await _context.Post
                .Where(p => p.SubcategoryId == SubcategoryId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();


            foreach (var post in posts)
            {
                var user = await _userManager.FindByIdAsync(post.UserId);
                if (user != null)
                {
                    var viewPost = new ViewPost
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        UserNickname = user.Nickname,
                        UserProfilePicture = user.ProfilePicture
                    };

                    Posts.Add(viewPost);
                }
            }
            return Page();
        }
    }
}

