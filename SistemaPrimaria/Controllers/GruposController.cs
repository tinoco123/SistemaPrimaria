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
    public class GruposController : Controller
    {
        private readonly SistemaPrimariaContext _context;

        public GruposController(SistemaPrimariaContext context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {

            List<Grupo> grupos = await _context.Grupo.ToListAsync();
            List<List<string>> datosGrupos = new List<List<string>>();
            foreach (var grupo in grupos)
            {
                List<string> datosGrupo = new List<string>();
                string listaMaterias = "";

                var nombreGrupo = grupo.NombreGrupo;
                var cedula = _context.Maestro.Find(grupo.IdMaestro).Cedula;
                var idMaterias = (from l in _context.GrupoMateria
                 where l.IdGrupo == grupo.Id
                 select l.IdMateria).ToList();
                // Traer los nombres de las materias
                foreach (int id in idMaterias)
                {
                    string nombreMateria = (from l in _context.Materia
                                            where l.Id == id
                                            select l.Nombre).ToList()[0];
                    listaMaterias += nombreMateria + "\n";
                }
                datosGrupo.Add(nombreGrupo);
                datosGrupo.Add(cedula);
                datosGrupo.Add(listaMaterias);

                datosGrupos.Add(datosGrupo);
            }
            ViewBag.datosGrupos = datosGrupos;
            ViewBag.grupos = grupos;
            return View();
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            List<MaestroViewModel> listaMaestros = null;

            listaMaestros = (from data in _context.Maestro
                           select new MaestroViewModel
                           {
                               Id = data.Id,
                               Cedula = data.Cedula
                           }).ToList();


            List<SelectListItem> maestros = listaMaestros.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Cedula.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            ViewBag.maestros = maestros;
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string nombreGrupo, int idMaestro)
        {
            List<MaestroViewModel> listaMaestros = null;

            listaMaestros = (from data in _context.Maestro
                             select new MaestroViewModel
                             {
                                 Id = data.Id,
                                 Cedula = data.Cedula
                             }).ToList();


            List<SelectListItem> maestros = listaMaestros.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Cedula.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            Grupo grupo = new Grupo();
            grupo.NombreGrupo = nombreGrupo;
            grupo.IdMaestro = idMaestro;

            _context.Add(grupo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));    
            
            
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreGrupo,IdMaestro")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id))
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
            return View(grupo);
        }

        // GET: Grupos/AsignarMaterias/5
        public async Task<IActionResult> AsignarMaterias(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }


            //Materias
            List<MateriasViewModel> listaMaterias = null;

            listaMaterias = (from data in _context.Materia
                             select new MateriasViewModel
                             {
                                 Id = data.Id,
                                 Nombre = data.Nombre
                             }).ToList();


            List<SelectListItem> materias = listaMaterias.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            

            ViewBag.materias = materias;
            ViewBag.grupo = grupo;

            return View();


        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarMaterias(int IdGrupo, int IdMateria)
        {
            //Materias
            List<MateriasViewModel> listaMaterias = null;

            listaMaterias = (from data in _context.Materia
                             select new MateriasViewModel
                             {
                                 Id = data.Id,
                                 Nombre = data.Nombre
                             }).ToList();


            List<SelectListItem> materias = listaMaterias.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            

            GrupoMateria grupoMateria = new GrupoMateria();
            grupoMateria.IdGrupo = IdGrupo;
            grupoMateria.IdMateria = IdMateria;
            _context.GrupoMateria.Add(grupoMateria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupo.FindAsync(id);
            _context.Grupo.Remove(grupo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupo.Any(e => e.Id == id);
        }
    }
}
