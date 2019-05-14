using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Veterinary.Web.Data.Entities;

namespace Veterinary.Web.Models
{
    public class HistoryViewModel : History
    {
        public int PetId { get; set; }

        [Required]
        [Display(Name = "Tipo Servicio *")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de servicio.")]
        public int ServiceTypeId { get; set; }

        public IEnumerable<SelectListItem> ServiceTypes { get; set; }
    }
}
