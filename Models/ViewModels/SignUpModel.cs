using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels
{
    public class SignUpModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
