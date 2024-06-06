using Snackis4.Areas.Identity.Data;

namespace Snackis4.Models
{
    public class ViewPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Snackis4User User { get; set; }
        public string UserId { get; set; }
        public string UserNickname { get; set; }
        public string UserProfilePicture { get; set; }

        public List<Comment> Comments { get; set; }

        public bool IsFlagged { get; set; }


        //visar bara max 100 ord av inlägget i underkategorin
        public string ShortContent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Content))
                    return string.Empty;

                var words = Content.Split(' ').Take(100);
                return string.Join(" ", words) + (words.Count() == 100 ? "..." : string.Empty);
            }
        }

    }
}
