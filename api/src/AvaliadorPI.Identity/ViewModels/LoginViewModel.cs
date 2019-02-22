using System.ComponentModel.DataAnnotations;

namespace AvaliadorPI.Identity.ViewModels
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; }
    }

    public class LoginInputModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
