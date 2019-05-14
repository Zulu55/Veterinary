namespace Veterinary.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class History
    {
        public int Id { get; set; }

        public ServiceType ServiceType { get; set; }

        [Display(Name = "Descripción *")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Desription { get; set; }

        [Display(Name = "Fecha Servicio *")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Observaciones")]
        public string Remarks { get; set; }
    }
}
