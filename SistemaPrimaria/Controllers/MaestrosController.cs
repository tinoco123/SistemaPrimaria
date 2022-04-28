using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaPrimaria.Data;
using SistemaPrimaria.Models;

namespace SistemaPrimaria.Controllers
{
    public class MaestrosController : Controller
    {
        private readonly SistemaPrimariaContext _context;

        public MaestrosController(SistemaPrimariaContext context)
        {
            _context = context;
        }

        // GET: Maestroes
        public async Task<IActionResult> Index()
        {
            List<Maestro> maestros = await _context.Maestro.ToListAsync();
            List<List<string>> datosMaestros = new List<List<string>>();
            foreach (var maestro in maestros)
            {
                List<string> datosMaestro = new List<string>();
                datosMaestro.Add(maestro.Cedula);
                datosMaestro.Add(maestro.Nombre);
                datosMaestro.Add(maestro.ApellidoMaterno);
                datosMaestro.Add(maestro.ApellidoPaterno);
                datosMaestro.Add(maestro.Telefono);
                datosMaestro.Add(maestro.Direccion);
                datosMaestro.Add((from l in _context.Grupo
                                  where l.IdMaestro == maestro.Id
                                  select l.NombreGrupo).ToList()[0]);
                
                datosMaestros.Add(datosMaestro);
            }
            ViewBag.datosMaestros = datosMaestros;
            ViewBag.maestros = maestros;
            return View();
        }

        // GET: Maestroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maestro == null)
            {
                return NotFound();
            }

            return View(maestro);
        }

        // GET: Maestroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maestroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,ApellidoMaterno,ApellidoPaterno,Telefono,Direccion")] Maestro maestro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maestro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maestro);
        }

        // GET: Maestroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestro.FindAsync(id);
            if (maestro == null)
            {
                return NotFound();
            }
            return View(maestro);
        }

        // POST: Maestroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,ApellidoMaterno,ApellidoPaterno,Telefono,Direccion")] Maestro maestro)
        {
            if (id != maestro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maestro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaestroExists(maestro.Id))
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
            return View(maestro);
        }

        // GET: Maestroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestro = await _context.Maestro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maestro == null)
            {
                return NotFound();
            }

            return View(maestro);
        }

        // POST: Maestroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maestro = await _context.Maestro.FindAsync(id);
            _context.Maestro.Remove(maestro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaestroExists(int id)
        {
            return _context.Maestro.Any(e => e.Id == id);
        }
    }
}
