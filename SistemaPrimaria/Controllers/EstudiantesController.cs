using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaPrimaria.Data;
using SistemaPrimaria.Models;
using System.IO;

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



        // GET: Estudiantes/AsignarGrupo/5
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

        public async Task<IActionResult> Boleta(int? id)
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

            //Consultar el grupo al que pertenece el estudiante 

            int idGrupo = (from l in _context.GrupoEstudiante
                         where l.IdEstudiante == id
                         select l.IdGrupo).ToList()[0];
           
            var grupoSelected = await _context.Grupo
                .FirstOrDefaultAsync(m => m.Id == idGrupo);

            //Consultar las materias del alumno
            var materias = (from l in _context.GrupoMateria
                            where l.IdGrupo == idGrupo
                            select l.IdMateria).ToList();

            List<Materia> ListaMateria = new List<Materia>();

            for (int i=0; i < materias.Count ; i++)
            {

                var materiaSelected = await _context.Materia
                .FirstOrDefaultAsync(m => m.Id == materias[i]);

                ListaMateria.Add(materiaSelected);
            }

            //Consultar las calificaciones de un alumno
            List<int> calificaciones = new List<int>();

            for (int d=0; d < materias.Count; d++)
            {
                int calificacion = (from l in _context.Calificacion
                                    where l.IdEstudiante == id
                                    where l.IdMateria == materias[d]
                                    select l.calificacion).ToList()[0];

                calificaciones.Add(calificacion);
            }

            ViewBag.grupo = grupoSelected;
            ViewBag.estudiante = estudiante;
            ViewBag.materia = ListaMateria;
            ViewBag.calificaciones = calificaciones;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Boleta(int idEstudiante)
        {

            //Consultar el grupo al que pertenece el estudiante 

            int idGrupo = (from l in _context.GrupoEstudiante
                           where l.IdEstudiante == idEstudiante
                           select l.IdGrupo).ToList()[0];

            var grupoSelected = await _context.Grupo
                .FirstOrDefaultAsync(m => m.Id == idGrupo);

            //Consultar las materias del alumno
            var materias = (from l in _context.GrupoMateria
                            where l.IdGrupo == idGrupo
                            select l.IdMateria).ToList();

            List<Materia> ListaMateria = new List<Materia>();

            for (int i = 0; i < materias.Count; i++)
            {

                var materiaSelected = await _context.Materia
                .FirstOrDefaultAsync(m => m.Id == materias[i]);

                ListaMateria.Add(materiaSelected);
            }

            //Consultar las calificaciones de un alumno
            List<int> calificaciones = new List<int>();

            for (int d = 0; d < materias.Count; d++)
            {
                int calificacion = (from l in _context.Calificacion
                                    where l.IdEstudiante == idEstudiante
                                    where l.IdMateria == materias[d]
                                    select l.calificacion).ToList()[0];

                calificaciones.Add(calificacion);
            }
            MemoryStream ms = new MemoryStream();
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter pw = PdfWriter.GetInstance(document, ms);
            document.Open();
           
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.BLUE);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk("Boleta de calificaciones".ToUpper(), fntHead));
            document.Add(prgHeading);

            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLUE);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Reporte Generatedo por : Sistema Primaria", fntAuthor));  
            prgGeneratedBY.Add(new Chunk("\nGenerado : " + DateTime.Now.ToShortDateString(), fntAuthor));  
            document.Add(prgGeneratedBY);

            //Adding a line  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Adding line break  
            document.Add(new Chunk("\n", fntHead));

            PdfPTable table = new PdfPTable(2);
            table.AddCell(new PdfPCell(new Phrase("Materias")));
            table.AddCell(new PdfPCell(new Phrase("Calificaciones")));
            for(int i =0; i<ListaMateria.Count; i++)
            {
                table.AddCell(new PdfPCell(new Phrase(ListaMateria[i].Nombre)));
                table.AddCell(new PdfPCell(new Phrase(calificaciones[i].ToString())));

            }
            document.Add(table);
            // Close the document  
            document.Close();
            // Close the writer instance  
            pw.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;
            return new FileStreamResult(ms,"application/pdf");

        }
        public DataTable GetDataTable(List<Materia> ListaMateria, List<int> calificaciones)
        {
            DataTable dt = new DataTable();
            // Headers
            dt.Columns.Add("Materia");
            dt.Columns.Add("Calificacion");
            //Rows
            for (int i = 0; i < ListaMateria.Count; i++)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Materia"] = ListaMateria[i].Nombre;
                dataRow["Calificacion"] = calificaciones[i];
            }
            return dt;
        }

        
    }

   
}
