using System.ComponentModel.DataAnnotations;

namespace BloggieWeb.Models.ViewModels
{
    public class LoginViewModels
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string? ReturnUrl { get; set; }
    }
}
