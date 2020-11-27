using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Wellness_USC.Models;

namespace Wellness_USC.Controllers
{
    public class ProfesoresController : Controller
    {
        private readonly ClaseDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProfesoresController(ClaseDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            this._hostEnviroment = hostEnviroment;
        }

        // GET: Profesors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.ProfesorId == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesors/Create
        [Authorize(Roles = "administrador,profesor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador,profesor")]
        public async Task<IActionResult> Create([Bind("ProfesorId,FirstName,LastName,FullName,ImageFile")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostEnviroment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(profesor.ImageFile.FileName);
                string extension = Path.GetExtension(profesor.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                profesor.ImageName = fileName;
                string path = Path.Combine(rootPath + "/assets/images/profesores/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await profesor.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesor);
        }

        // GET: Profesors/Edit/5
        [Authorize(Roles = "administrador,profesor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View(profesor);
        }

        // POST: Profesors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador,profesor")]
        public async Task<IActionResult> Edit(int id, [Bind("ProfesorId,FirstName,LastName,FullName,ImageName")] Profesor profesor)
        {
            if (id != profesor.ProfesorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.ProfesorId))
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
            return View(profesor);
        }

        // GET: Profesors/Delete/5
        [Authorize(Roles = "administrador,profesor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.ProfesorId == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador,profesor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            var imagePath = Path.Combine(_hostEnviroment.WebRootPath + "/assets/images/profesores/", profesor.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesores.Any(e => e.ProfesorId == id);
        }
    }
}