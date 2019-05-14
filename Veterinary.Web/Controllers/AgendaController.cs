namespace Veterinary.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Veterinary.Web.Data;
    using Veterinary.Web.Data.Entities;
    using Veterinary.Web.Models;

    [Authorize]
    public class AgendaController : Controller
    {
        private readonly DataContext _context;

        public AgendaController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var agendas = await _context.Agendas
                .Where(a => a.Date >= DateTime.Today && a.Date <= DateTime.Today.AddDays(7))
                .Include(a => a.Owner)
                .Include(a => a.Pet)
                .OrderBy(a => a.Date)
                .ToListAsync();
            return View(agendas);
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas.FindAsync(id.Value);
            if (agenda == null)
            {
                return NotFound();
            }

            agenda.Owner = null;
            agenda.Pet = null;
            agenda.Remarks = null;
            agenda.IsAvailable = true;
            this._context.Agendas.Update(agenda);
            await this._context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Assing(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas.FindAsync(id.Value);
            if (agenda == null)
            {
                return NotFound();
            }

            var view = new AgendaViewModel
            {
                Id = id.Value,
                Owners = this.GetComboOwners(),
                Pets = this.GetComboPets()
            };

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> Assing(AgendaViewModel view)
        {
            if (ModelState.IsValid)
            {
                var agenda = await this._context.Agendas.FindAsync(view.Id);
                if (agenda != null)
                {
                    var owner = await this._context.Owners.FindAsync(view.OwnerId);
                    var pet = await this._context.Pets.FindAsync(view.PetId);
                    agenda.IsAvailable = false;
                    agenda.Owner = owner;
                    agenda.Pet = pet;
                    agenda.Remarks = view.Remarks;
                    this._context.Agendas.Update(agenda);
                    await this._context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(view);
        }

        private IEnumerable<SelectListItem> GetComboPets()
        {
            var list = this._context.Pets.Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = p.Id.ToString()
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una mascota...)",
                Value = "0"
            });

            return list;
        }

        private IEnumerable<SelectListItem> GetComboOwners()
        {
            var list = this._context.Owners.Select(p => new SelectListItem
            {
                Text = p.FullNameWithDocument,
                Value = p.Id.ToString()
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un propietario...)",
                Value = "0"
            });

            return list;
        }
    }
}