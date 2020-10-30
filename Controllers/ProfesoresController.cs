using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wellness_USC.Models;

namespace Wellness_USC.Controllers
{
    public class ProfesoresController : Controller
    {
        private readonly ProfesoresDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProfesoresController(ProfesoresDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            this._hostEnviroment = hostEnviroment;
        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesoress.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesoress
                .FirstOrDefaultAsync(m => m.Id_Profesores == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Profesores,Name,Apell,ImageFileP")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostEnviroment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(profesores.ImageFileP.FileName);
                string extension = Path.GetExtension(profesores.ImageFileP.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                profesores.ImageNameP = fileName;
                string path = Path.Combine(rootPath + "/assets/images/profesoress/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await profesores.ImageFileP.CopyToAsync(fileStream);
                }
                _context.Add(profesores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesores);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesoress.FindAsync(id);
            if (profesores == null)
            {
                return NotFound();
            }
            return View(profesores);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Profesores,Name,Apell,ImageNameP")] Profesores profesores)
        {
            if (id != profesores.Id_Profesores)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresExists(profesores.Id_Profesores))
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
            return View(profesores);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesores = await _context.Profesoress
                .FirstOrDefaultAsync(m => m.Id_Profesores == id);
            if (profesores == null)
            {
                return NotFound();
            }

            return View(profesores);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesores = await _context.Profesoress.FindAsync(id);
            var imagePath = Path.Combine(_hostEnviroment.WebRootPath + "/assets/images/profesoress/", profesores.ImageNameP);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.Profesoress.Remove(profesores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesoress.Any(e => e.Id_Profesores == id);
        }
    }
}
