using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public string? UserId { get; set; }
        
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
        
        
        [Required(ErrorMessage = "Please enter a name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string? State { get; set; }

        public string? Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string? Country { get; set; }

        public bool GiftWrap { get; set; }

        [BindNever]
        public IdentityUser? User { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
        [BindNever]
        public DateTime? Date { get; set; } = DateTime.Now;
    }
}
