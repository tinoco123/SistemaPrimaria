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
    public class CalificacionesController : Controller
    {
        private readonly SistemaPrimariaContext _context;

        public CalificacionesController(SistemaPrimariaContext context)
        {
            _context = context;
        }

        // GET: Calificacions
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Calificacion.ToListAsync());
        //}

        

        // GET: Calificacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,calificacion,IdEstudiante,IdMateria")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
        }

        

        private bool CalificacionExists(int id)
        {
            return _context.Calificacion.Any(e => e.Id == id);
        }
    }
}
