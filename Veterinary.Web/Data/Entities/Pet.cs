namespace Veterinary.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Pet
    {
        public int Id { get; set; }

        [Display(Name = "Nombre *")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Raza")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Race { get; set; }

        public Owner Owner { get; set; }

        public PetType PetType { get; set; }

        [Display(Name = "Fecha Nacimiento *")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }

        [Display(Name = "Observaciones")]
        public string Remarks { get; set; }

        public ICollection<History> Histories { get; set; }
    }
}
