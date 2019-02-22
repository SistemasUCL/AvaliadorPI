using System.ComponentModel.DataAnnotations;

namespace AvaliadorPI.Identity.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "A senha deve conter mais que 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "A senha deve conter menos que 100 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [Compare("NewPassword", ErrorMessage = "A senha de confirmação deve ser igual à nova senha.")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        public string StatusMessage { get; set; }
    }
}
