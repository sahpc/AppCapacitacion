using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppCapacitacion.Data;
using AppCapacitacion.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppCapacitacion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructores.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructores.FirstOrDefaultAsync(i => i.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructores.FindAsync(id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructores.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructores.FirstOrDefaultAsync(i => i.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructores.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructores.Remove(instructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
