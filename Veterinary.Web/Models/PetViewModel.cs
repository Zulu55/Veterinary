namespace Veterinary.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PetViewModel : Pet
    {
        [Required]
        [Display(Name = "Propietario *")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un propietario.")]
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "Tipo Mascota *")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de mascota.")]
        public int PetTypeId { get; set; }

        public IEnumerable<SelectListItem> Owners { get; set; }

        public IEnumerable<SelectListItem> PetTypes { get; set; }
    }
}