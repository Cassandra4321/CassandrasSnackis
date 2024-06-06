using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Message
{
    public class CreateModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public CreateModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string? UserReceiverId {  get; set; }

        [BindProperty]
        public string? MessageContent { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/account/login");
            }

            var userSenderId = _userManager.GetUserId(User);

            var message = new PrivateMessages
            {
                MessageContent = MessageContent,
                SentAt = DateTime.Now,
                UserSenderId = userSenderId,
                UserReceiverId = UserReceiverId

            };

                _context.PrivateMessages.Add(message);
                await _context.SaveChangesAsync();

                return RedirectToPage("/message/inbox");


        }
    }
}
