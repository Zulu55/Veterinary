namespace Veterinary.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Owner
    {
        public int Id { get; set; }

        [Display(Name = "Número Documento *")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Document { get; set; }

        [Display(Name = "Nombres *")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos *")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string FixedPhone { get; set; }

        [Display(Name = "Teléfono Celular")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string CellPhone { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Address { get; set; }

        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        public string FullNameWithDocument { get { return $"{this.FirstName} {this.LastName} - {this.Document}"; } }
    }
}