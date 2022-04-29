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

            

        // GET: Calificacions/Create
        public IActionResult Create()
        {

            //Estudiantes
            List<EstudianteViewModel> listaEstudiantes = null;

            listaEstudiantes = (from data in _context.Estudiante
                           select new EstudianteViewModel
                           {
                               Id = data.Id,
                               Matricula = data.Matricula
                           }).ToList();


            List<SelectListItem> estudiantes = listaEstudiantes.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Matricula.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.estudiantes = estudiantes;

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


            return View();

        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( int idEstudiante, int idMateria,int calificacion )
        {
            //Estudiante


            List<EstudianteViewModel> listaEstudiantes = null;

            listaEstudiantes = (from data in _context.Estudiante
                                select new EstudianteViewModel
                                {
                                    Id = data.Id,
                                    Matricula = data.Matricula
                                }).ToList();


            List<SelectListItem> estudiantes = listaEstudiantes.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Matricula.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.estudiantes = estudiantes;

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


            Calificacion calificaciones = new Calificacion();
            calificaciones.IdEstudiante = idEstudiante;
            calificaciones.IdMateria = idMateria;
            calificaciones.calificacion = calificacion;

            
            _context.Calificacion.Add(calificaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
            
        }


        

        private bool CalificacionExists(int id)
        {
            return _context.Calificacion.Any(e => e.Id == id);
        }
    }
}
