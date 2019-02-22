using System.ComponentModel.DataAnnotations;

namespace AvaliadorPI.Identity.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "E-mail invalido.")]
        public string Username { get; set; }

        public bool TrocarSenha { get; set; }
        public bool Adminsitrador { get; set; }
        public bool Avaliador { get; set; }

        public string StatusMessage { get; set; }
    }
}
