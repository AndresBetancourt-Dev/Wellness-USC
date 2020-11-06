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
    public class RegistrosController : Controller
    {
        private readonly ClaseDbContext _context;

        public RegistrosController(ClaseDbContext context)
        {
            _context = context;
        }

        // GET: Registroes
        public async Task<IActionResult> Index()
        {
            var claseDbContext = _context.Registros.Include(r => r.Clase).Include(r => r.User);
            return View(await claseDbContext.ToListAsync());
        }

        // GET: Registroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registros
                .Include(r => r.Clase)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RegistroId == id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        // GET: Registroes/Create
        public IActionResult Create()
        {
            ViewData["ClaseId"] = new SelectList(_context.Clases, "ClaseId", "Name");
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Registroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroId,Id,ClaseId")] Registro registro)
        {
            Console.WriteLine(registro.ClaseId);
            var row = await _context.Clases.FirstOrDefaultAsync(clase => clase.ClaseId == registro.ClaseId);
            Console.WriteLine(row.Quantity);

            var rows = _context.Registros.Where(r => r.ClaseId == row.ClaseId).ToList();

            Console.WriteLine(rows.Count);

            if (rows.Count >= row.Quantity)
            {
                Console.WriteLine("Maximo Excedido");
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _context.Add(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaseId"] = new SelectList(_context.Clases, "ClaseId", "Name", registro.ClaseId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", registro.Id);
            return View(registro);
        }

        // GET: Registroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }
            ViewData["ClaseId"] = new SelectList(_context.Clases, "ClaseId", "Name", registro.ClaseId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", registro.Id);
            return View(registro);
        }

        // POST: Registroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistroId,Id,ClaseId")] Registro registro)
        {
            if (id != registro.RegistroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroExists(registro.RegistroId))
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
            ViewData["ClaseId"] = new SelectList(_context.Clases, "ClaseId", "Name", registro.ClaseId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", registro.Id);
            return View(registro);
        }

        // GET: Registroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro = await _context.Registros
                .Include(r => r.Clase)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RegistroId == id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        // POST: Registroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registro = await _context.Registros.FindAsync(id);
            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroExists(int id)
        {
            return _context.Registros.Any(e => e.RegistroId == id);
        }
    }
}
