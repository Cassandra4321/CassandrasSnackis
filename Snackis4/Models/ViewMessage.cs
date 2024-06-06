namespace Snackis4.Models
{
    public class ViewMessage
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderNickname { get; set; }
    }
}
