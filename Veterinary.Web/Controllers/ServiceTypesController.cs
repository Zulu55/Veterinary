using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinary.Web.Data;
using Veterinary.Web.Data.Entities;

namespace Veterinary.Web.Controllers
{
    [Authorize]
    public class ServiceTypesController : Controller
    {
        private readonly DataContext _context;

        public ServiceTypesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTypeExists(serviceType.Id))
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
            return View(serviceType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTypeExists(int id)
        {
            return _context.ServiceTypes.Any(e => e.Id == id);
        }
    }
}