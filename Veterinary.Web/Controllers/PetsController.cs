namespace Veterinary.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Veterinary.Web.Models;

    [Authorize]
    public class PetsController : Controller
    {
        private readonly DataContext _context;

        public PetsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Pets.Include(p => p.Owner).Include(p => p.PetType).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.PetType)
                .Where(p => p.Id == id.Value)
                .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IActionResult Create()
        {
            var view = new PetViewModel
            {
                Born = DateTime.Now,
                Owners = this.GetComboOwners(),
                PetTypes = this.GetComboPetTypes()
            };

            return View(view);
        }

        private IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var list = this._context.PetTypes.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un tipo de mascota...)",
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
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un propietario...)",
                Value = "0"
            });

            return list;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pet = await this.ToPet(view);
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private async Task<Pet> ToPet(PetViewModel view)
        {
            var owner = await this._context.Owners.FindAsync(view.OwnerId);
            var petType = await this._context.PetTypes.FindAsync(view.PetTypeId);

            return new Pet
            {
                Born = view.Born,
                Id = view.Id,
                Nombre = view.Nombre,
                Owner = owner,
                PetType = petType,
                Race = view.Race,
                Remarks = view.Remarks
            };
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.PetType)
                .Where(p => p.Id == id.Value)
                .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }

            var view = this.ToPetViewModel(pet);
            return View(view);
        }

        private PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Born = pet.Born,
                Id = pet.Id,
                Nombre = pet.Nombre,
                Owner = pet.Owner,
                OwnerId = pet.Owner.Id,
                Owners = this.GetComboOwners(),
                PetType = pet.PetType,
                PetTypeId = pet.PetType.Id,
                PetTypes = this.GetComboPetTypes(),
                Race = pet.Race,
                Remarks = pet.Remarks
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PetViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pet = await this.ToPet(view);
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.PetType)
                .Where(p => p.Id == id.Value)
                .FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}