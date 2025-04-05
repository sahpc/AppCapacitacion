using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCapacitacion.Data;
using AppCapacitacion.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppCapacitacion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EvaluacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evaluacion
        public async Task<IActionResult> Index()
        {
            var evaluaciones = _context.Evaluaciones
                .Include(e => e.Empleado)
                .Include(e => e.Capacitacion);
            return View(await evaluaciones.ToListAsync());
        }

        // GET: Evaluacion/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre");
            ViewData["CapacitacionId"] = new SelectList(_context.Capacitaciones, "Id", "Nombre");
            return View();
        }

        // POST: Evaluacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarga combos si hay error
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", evaluacion.EmpleadoId);
            ViewData["CapacitacionId"] = new SelectList(_context.Capacitaciones, "Id", "Nombre", evaluacion.CapacitacionId);
            return View(evaluacion);
        }


        // GET: Evaluacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evaluacion = await _context.Evaluaciones
                .Include(e => e.Empleado)
                .Include(e => e.Capacitacion)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evaluacion == null) return NotFound();

            return View(evaluacion);
        }

        // Otros métodos Edit/Delete los puedes agregar después si los necesitas
    }
}
