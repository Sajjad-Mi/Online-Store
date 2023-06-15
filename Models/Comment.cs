using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public class Comment
    {
        [BindNever]
        public int CommentId { get; set; }

        [BindNever]
        public string? UserId { get; set; }
        
        public long? ProductId { get; set; }

        [Required]
        public string Text { get; set; }
    
        [BindNever]
        public IdentityUser? User { get; set; }
        [BindNever]
        public Product? Product { get; set; }
        [BindNever]
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
