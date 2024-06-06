using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis4.Areas.Identity.Data;
using Snackis4.Data;
using Snackis4.Models;

namespace Snackis4.Pages.Message
{
    public class InboxModel : PageModel
    {
        private readonly Snackis4Context _context;
        private readonly UserManager<Snackis4User> _userManager;

        public InboxModel(Snackis4Context context, UserManager<Snackis4User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ViewMessage> Messages { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            var messages = await _context.PrivateMessages
                .Where(m => m.UserReceiverId == userId)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();

            Messages = new List<ViewMessage>();

            foreach (var message in messages)
            {
                var sender = await _userManager.FindByIdAsync(message.UserSenderId);
                Messages.Add(new ViewMessage
                {
                    Id = message.Id,
                    MessageContent = message.MessageContent,
                    SentAt = message.SentAt,
                    SenderNickname = sender.Nickname
                });
            }
        }
    }
}
