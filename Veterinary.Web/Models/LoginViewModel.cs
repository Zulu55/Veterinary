using System.ComponentModel.DataAnnotations;

namespace Veterinary.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Email *")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Contraseña *")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}
