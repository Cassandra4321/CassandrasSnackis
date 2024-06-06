using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;
using System.Security.Claims;
using System.Xml.Linq;

namespace Snackis4.Pages.Forum
{
    public class ShowPostAndCommentsModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public ShowPostAndCommentsModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ViewPost Post { get; set; }
        public int PostId { get; set; }

        public List<Comment> Comments { get; set; }

        public Snackis4User CurrentUser { get; set; }


        public async Task<IActionResult> OnGetAsync(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            PostId = postId.Value;

            var post = await _context.Post
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(post.UserId);
            if (user == null)
            {
                return NotFound();
            }

            Post = new ViewPost
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UserId = post.UserId,
                UserNickname = user.Nickname,
                UserProfilePicture = user.ProfilePicture,
                Comments = post.Comments
            };

            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int postId, string content)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }
            PostId = postId;

            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.Now,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                PostId = postId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forum/ShowPostAndComments");
        }


        public async Task<IActionResult> OnPostReportPostAsync(int postId)
        {
            var post = await _context.Post.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            post.IsFlagged = true;
            await _context.SaveChangesAsync();

            return RedirectToPage(new { postId });
        }

        public async Task<IActionResult> OnPostReportCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            comment.IsFlagged = true;
            await _context.SaveChangesAsync();


            var post = await _context.Post.FindAsync(comment.PostId);
            if (post == null)
            {
                return NotFound();
            }

            PostId = post.Id;

            return RedirectToPage(new { postId = PostId });
        }


    }
}
