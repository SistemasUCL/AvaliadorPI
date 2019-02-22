using System.ComponentModel.DataAnnotations;

namespace AvaliadorPI.Identity.ViewModels
{
    public class NewUserViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "E-mail invalido.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Id do Usuario")]
        public string UsuarioId { get; set; }

        public bool Adminsitrador { get; set; }
        public bool Avaliador { get; set; }

        public string StatusMessage { get; set; }
    }
}
