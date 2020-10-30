using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wellness_USC.Models;

namespace Wellness_USC.Controllers
{
    public class ClasesController : Controller
    {
        private readonly ClaseDbContext _context;

        public ClasesController(ClaseDbContext context)
        {
            _context = context;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var claseDbContext = _context.Clase.Include(c => c.Course).Include(c => c.Horario).Include(c => c.Profesores);
            return View(await claseDbContext.ToListAsync());
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.Course)
                .Include(c => c.Horario)
                .Include(c => c.Profesores)
                .FirstOrDefaultAsync(m => m.Id_Clase == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {
            ViewData["CoursesId"] = new SelectList(_context.Set<Course>(), "Id", "Id");
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "Id_Horario", "Id_Horario");
            ViewData["ProfesoresId"] = new SelectList(_context.Set<Profesores>(), "Id_Profesores", "Id_Profesores");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Clase,cantidad,Type,CoursesId,HorarioId,ProfesoresId")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursesId"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.CoursesId);
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "Id_Horario", "Id_Horario", clase.HorarioId);
            ViewData["ProfesoresId"] = new SelectList(_context.Set<Profesores>(), "Id_Profesores", "Id_Profesores", clase.ProfesoresId);
            return View(clase);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            ViewData["CoursesId"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.CoursesId);
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "Id_Horario", "Id_Horario", clase.HorarioId);
            ViewData["ProfesoresId"] = new SelectList(_context.Set<Profesores>(), "Id_Profesores", "Id_Profesores", clase.ProfesoresId);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Clase,cantidad,Type,CoursesId,HorarioId,ProfesoresId")] Clase clase)
        {
            if (id != clase.Id_Clase)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.Id_Clase))
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
            ViewData["CoursesId"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.CoursesId);
            ViewData["HorarioId"] = new SelectList(_context.Set<Horario>(), "Id_Horario", "Id_Horario", clase.HorarioId);
            ViewData["ProfesoresId"] = new SelectList(_context.Set<Profesores>(), "Id_Profesores", "Id_Profesores", clase.ProfesoresId);
            return View(clase);
        }

        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.Course)
                .Include(c => c.Horario)
                .Include(c => c.Profesores)
                .FirstOrDefaultAsync(m => m.Id_Clase == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clase.FindAsync(id);
            _context.Clase.Remove(clase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
            return _context.Clase.Any(e => e.Id_Clase == id);
        }
    }
}
