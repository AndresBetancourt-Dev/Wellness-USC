using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wellness_USC.Areas.Identity.Data;
using static SweetAlertBlog.Enums.Enums;
using Microsoft.AspNetCore.Authorization;

using Wellness_USC.Models;
using Wellness_USC.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;

namespace Wellness_USC.Controllers
{
    public class RegistrosController : BaseController
    {
        private readonly ClaseDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegistrosController(ClaseDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

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
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "FirstName");

            return View();
        }

        // POST: Registroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistroId,Id,ClaseId")] Registro registro)
        {

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userExists = await _context.Registros.FirstOrDefaultAsync(r => (r.ClaseId == registro.ClaseId && r.Id == user.Id));
            if (userExists != null)
            {
                Alert("Usted ya se ha registrado en este curso", NotificationType.error);
                return RedirectToAction(nameof(Index));
            }

            var row = await _context.Clases.FirstOrDefaultAsync(clase => clase.ClaseId == registro.ClaseId);
            var rows = _context.Registros.Where(r => r.ClaseId == row.ClaseId).ToList();


            if (rows.Count >= row.Quantity)
            {
                Alert("Lo sentimos, el Curso ha Excedido la Cantidad De Estudiantes", NotificationType.error);
                return RedirectToAction(nameof(Index));
            }

            var newRegistro = new Registro { Id = user.Id, ClaseId = registro.ClaseId };

            if (ModelState.IsValid)
            {
                _context.Add(newRegistro);
                await _context.SaveChangesAsync();
                Alert("Felicitaciones, Te has registrado exitosamente en esta Clase", NotificationType.success);
                return RedirectToAction(nameof(Index));
            }


            ViewData["ClaseId"] = new SelectList(_context.Clases, "ClaseId", "Name", registro.ClaseId);
            ViewData["Id"] = new SelectList(_context.AspNetUsers, "Id", "Id", registro.Id);
            return View(registro);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Registrar(int? id)
        {
            if (id == null)
            {
                Alert("Lo sentimos, Esta Clase No Existe", NotificationType.error);
                return RedirectToAction("Index");
            }

            var clase = await _context.Clases.FirstOrDefaultAsync(c => c.ClaseId == id);
            if (clase == null)
            {
                Alert("Lo sentimos, Esta Clase No Existe", NotificationType.error);
                return RedirectToAction("Index", "Clases");
            }

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var registro = new RegistrosViewModel { UserId = user.Id, ClaseId = clase.ClaseId, Name = user.FirstName, Surname = user.LastName, Clase = clase.Name };
            return View(registro);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Registrar(RegistrosViewModel registro)
        {
            var userExists = await _context.Registros.FirstOrDefaultAsync(r => (r.ClaseId == registro.ClaseId && r.Id == registro.UserId));
            if (userExists != null)
            {

                return RedirectToAction("Denegado");
            }

            var row = await _context.Clases.FirstOrDefaultAsync(clase => clase.ClaseId == registro.ClaseId);

            var rows = _context.Registros.Where(r => r.ClaseId == row.ClaseId).ToList();


            if (rows.Count >= row.Quantity)
            {

                return RedirectToAction("Excedido");
            }

            var newRegistro = new Registro { Id = registro.UserId, ClaseId = registro.ClaseId };

            if (ModelState.IsValid)
            {
                _context.Add(newRegistro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Registrado");
            }

            return View(registro);
        }

        public IActionResult Registrado()
        {


            return View();
        }

        public IActionResult Denegado()
        {


            return View();
        }

        public IActionResult Excedido()
        {


            return View();
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
