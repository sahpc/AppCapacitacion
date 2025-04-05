using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCapacitacion.Models;

namespace AppCapacitacion.Controllers
{

    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Admin")]
    public class CapacitacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapacitacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Capacitacion
        public async Task<IActionResult> Index()
        {
            var capacitaciones = _context.Capacitaciones.Include(c => c.Instructor);
            return View(await capacitaciones.ToListAsync());
        }


        // GET: Capacitacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var capacitacion = await _context.Capacitaciones
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (capacitacion == null) return NotFound();

            return View(capacitacion);
        }


        // GET: Capacitacion/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructores, "Id", "Nombre");
            return View();
        }




        // POST: Capacitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Capacitacion capacitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(capacitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InstructorId"] = new SelectList(_context.Instructores, "Id", "Nombre", capacitacion.InstructorId);
            return View(capacitacion);
        }



        // GET: Capacitacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacitacion = await _context.Capacitaciones.FindAsync(id);
            if (capacitacion == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructores, "Id", "Nombre", capacitacion.InstructorId);

            return View(capacitacion);
        }

        // POST: Capacitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Fecha,InstructorId")] Capacitacion capacitacion)
        {
            if (id != capacitacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(capacitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CapacitacionExists(capacitacion.Id))
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
            ViewData["InstructorId"] = new SelectList(_context.Instructores, "Id", "Id", capacitacion.InstructorId);
            return View(capacitacion);
        }

        // GET: Capacitacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacitacion = await _context.Capacitaciones
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (capacitacion == null)
            {
                return NotFound();
            }

            return View(capacitacion);
        }

        // POST: Capacitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capacitacion = await _context.Capacitaciones.FindAsync(id);
            if (capacitacion != null)
            {
                _context.Capacitaciones.Remove(capacitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CapacitacionExists(int id)
        {
            return _context.Capacitaciones.Any(e => e.Id == id);
        }
    }
}
