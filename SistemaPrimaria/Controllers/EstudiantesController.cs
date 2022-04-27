﻿using System;
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
    public class EstudiantesController : Controller
    {
        private readonly SistemaPrimariaContext _context;

        public EstudiantesController(SistemaPrimariaContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudiante.ToListAsync());
        }



        // GET: Estudiantes/AsjgnarGrupo/5
        public async Task<IActionResult> AsignarGrupo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            List<GrupoViewModel> listaGrupos = null;

            listaGrupos = (from data in _context.Grupo
                           select new GrupoViewModel
                           {
                               Id = data.Id,
                               NombreGrupo = data.NombreGrupo
                           }).ToList();


            List<SelectListItem> grupos = listaGrupos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreGrupo.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.grupos = grupos;
            ViewBag.estudiante = estudiante;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarGrupo( int IdGrupo, int IdEstudiante)
        {
            
            
            List<GrupoViewModel> listaGrupos = null;

            listaGrupos = (from data in _context.Grupo
                           select new GrupoViewModel
                           {
                               Id = data.Id,
                               NombreGrupo = data.NombreGrupo
                           }).ToList();


            List<SelectListItem> grupos = listaGrupos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreGrupo.ToString(),
                    Value = d.Id.ToString(),
                    Selected = true
                };
            });

            GrupoEstudiante grupoEstudiante = new GrupoEstudiante();
            
            grupoEstudiante.IdEstudiante = IdEstudiante;
            grupoEstudiante.IdGrupo = IdGrupo;
            
            int id_estudiante = IdEstudiante;
            int id_grupo = IdGrupo;

            _context.GrupoEstudiante.Add(grupoEstudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Matricula,Nombre,ApellidoMaterno,ApellidoPaterno,Telefono,Direccion")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Nombre,ApellidoMaterno,ApellidoPaterno,Telefono,Direccion")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.Id == id);
        }
    }
}
