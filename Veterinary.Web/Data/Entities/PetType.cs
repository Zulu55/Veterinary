namespace Veterinary.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class PetType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo Mascota *")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
    }
}