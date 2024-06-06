using Microsoft.AspNetCore.Mvc.ModelBinding;
using Snackis4.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snackis4.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(3000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        //public Snackis4User User { get; set; }
        public string? UserId { get; set; }

        //public Subcategory Subcategory { get; set; }
        public int? SubcategoryId { get; set; }

        public bool IsFlagged { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}

//public class Post
//{
//    public int Id { get; set; }

//    [Required]
//    public string UserId { get; set; }

//    [Required]
//    [StringLength(100)]
//    public string Title { get; set; }

//    [Required]
//    public string Content { get; set; }

//    public DateTime TimeStamp { get; set; }

//    public int SubcategoryId { get; set; }

//    public Subcategory Subcategory { get; set; }

//    public bool IsFlagged { get; set; }
//}

