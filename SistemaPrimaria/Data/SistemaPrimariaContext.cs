using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaPrimaria.Models;

namespace SistemaPrimaria.Data
{
    public class SistemaPrimariaContext : DbContext
    {
        public SistemaPrimariaContext (DbContextOptions<SistemaPrimariaContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaPrimaria.Models.Estudiante> Estudiante { get; set; }

        public DbSet<SistemaPrimaria.Models.Materia> Materia { get; set; }

        public DbSet<SistemaPrimaria.Models.Maestro> Maestro { get; set; }

        public DbSet<SistemaPrimaria.Models.Calificacion> Calificacion { get; set; }

        public DbSet<SistemaPrimaria.Models.Grupo> Grupo { get; set; }
        
        public DbSet<SistemaPrimaria.Models.GrupoEstudiante> GrupoEstudiante { get; set; }

        public DbSet<SistemaPrimaria.Models.GrupoMateria> GrupoMateria { get; set; }


    }
}
