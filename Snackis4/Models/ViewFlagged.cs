namespace Snackis4.Models
{
    public class ViewFlagged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public string AuthorNickname { get; set; }
        public DateTime AuthorCreatedAt { get; set; }
    }
}
