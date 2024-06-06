using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;
using System.Xml.Linq;

namespace Snackis4.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class FlaggedModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public FlaggedModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ViewFlagged> FlaggedPosts { get; set; }
        public List<ViewFlagged> FlaggedComments { get; set; }
        public async Task OnGetAsync()
        {
            var flaggedPosts = await _context.Post.Where(p => p.IsFlagged).ToListAsync();
            FlaggedPosts = new List<ViewFlagged>();

            foreach (var post in flaggedPosts)
            {
                var user = await _userManager.FindByIdAsync(post.UserId);
                var author = await _userManager.FindByIdAsync(post.UserId);
                FlaggedPosts.Add(new ViewFlagged
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    AuthorId = author?.Id,
                    AuthorNickname = author.Nickname,
                    AuthorCreatedAt = post.CreatedAt
                });
            }

            var flaggedComments = await _context.Comments.Where(c => c.IsFlagged).ToListAsync();
            FlaggedComments = new List<ViewFlagged>();

            foreach (var comment in flaggedComments)
            {
                var user = await _userManager.FindByIdAsync(comment.UserId);
                var author = await _userManager.FindByIdAsync(comment.UserId);
                FlaggedComments.Add(new ViewFlagged
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    AuthorId = author?.Id,
                    AuthorNickname = author.Nickname,
                    AuthorCreatedAt = comment.CreatedAt
                });
            }
        }



        public async Task<IActionResult> OnPostRemovePostAsync(int postId)
        {
            var post = await _context.Post.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}