using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Forum
{
    public class CreatePostModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public CreatePostModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Post NewPost { get; set; }


        [BindProperty]
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }


        public async Task<IActionResult> OnGetAsync(int subcategoryId)
        {
            Subcategory = await _context.Subcategory.FirstOrDefaultAsync(s => s.Id == subcategoryId);
            if (Subcategory == null)
            {
                return NotFound();
            }

            SubcategoryId = subcategoryId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            NewPost.UserId = user.Id;
            NewPost.SubcategoryId = SubcategoryId;
            NewPost.CreatedAt = DateTime.Now;
            NewPost.IsFlagged = false;

            _context.Post.Add(NewPost);
            await _context.SaveChangesAsync();



            return RedirectToPage("/Forum/ShowPosts", new { subcategoryId = SubcategoryId });
        }
    }
}


