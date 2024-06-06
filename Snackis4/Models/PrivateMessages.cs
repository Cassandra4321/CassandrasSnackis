namespace Snackis4.Models
{
    public class PrivateMessages
    {
        public int Id { get; set; }
        public DateTime SentAt { get; set; }
        public string MessageContent { get; set; }

        public string UserSenderId { get; set; }
        public string UserReceiverId { get; set; }
    }
}
