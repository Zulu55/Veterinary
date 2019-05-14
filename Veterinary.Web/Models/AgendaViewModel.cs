namespace Veterinary.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AgendaViewModel : Agenda
    {
        [Required]
        [Display(Name = "Propietario *")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un propietario.")]
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "Mascota *")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una mascota.")]
        public int PetId { get; set; }

        public IEnumerable<SelectListItem> Owners { get; set; }

        public IEnumerable<SelectListItem> Pets { get; set; }
    }
}