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
        private readonly ProfesoresDbContext _context;

        public ClasesController(ProfesoresDbContext context)
        {
            _context = context;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var profesoresDbContext = _context.Clase.Include(c => c.Courses).Include(c => c.Horarios).Include(c => c.Profesoress);
            return View(await profesoresDbContext.ToListAsync());
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.Courses)
                .Include(c => c.Horarios)
                .Include(c => c.Profesoress)
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
            ViewData["Id"] = new SelectList(_context.Set<Course>(), "Id", "Id");
            ViewData["Id_Horario"] = new SelectList(_context.Horario, "Id_Horario", "Id_Horario");
            ViewData["Id_Profesores"] = new SelectList(_context.Profesoress, "Id_Profesores", "Id_Profesores");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Clase,cantidad,Type,Id,Id_Horario,Id_Profesores")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.Id);
            ViewData["Id_Horario"] = new SelectList(_context.Horario, "Id_Horario", "Id_Horario", clase.Id_Horario);
            ViewData["Id_Profesores"] = new SelectList(_context.Profesoress, "Id_Profesores", "Id_Profesores", clase.Id_Profesores);
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
            ViewData["Id"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.Id);
            ViewData["Id_Horario"] = new SelectList(_context.Horario, "Id_Horario", "Id_Horario", clase.Id_Horario);
            ViewData["Id_Profesores"] = new SelectList(_context.Profesoress, "Id_Profesores", "Id_Profesores", clase.Id_Profesores);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Clase,cantidad,Type,Id,Id_Horario,Id_Profesores")] Clase clase)
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
            ViewData["Id"] = new SelectList(_context.Set<Course>(), "Id", "Id", clase.Id);
            ViewData["Id_Horario"] = new SelectList(_context.Horario, "Id_Horario", "Id_Horario", clase.Id_Horario);
            ViewData["Id_Profesores"] = new SelectList(_context.Profesoress, "Id_Profesores", "Id_Profesores", clase.Id_Profesores);
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
                .Include(c => c.Courses)
                .Include(c => c.Horarios)
                .Include(c => c.Profesoress)
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
