using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Message
{
    public class SentModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public SentModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ViewMessage> Messages { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            var messages = await _context.PrivateMessages
                .Where(m => m.UserSenderId == userId)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();

            Messages = new List<ViewMessage>();

            foreach (var message in messages)
            {
                var receiver = await _userManager.FindByIdAsync(message.UserReceiverId);
                Messages.Add(new ViewMessage
                {
                    Id = message.Id,
                    MessageContent = message.MessageContent,
                    SentAt = message.SentAt,
                    SenderNickname = receiver.Nickname
                });
            }
        }
    }
}
