using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaPrimaria.Models;

namespace SistemaPrimaria.Data
{
    public class SistemaPrimariaDB : DbContext
    {
        public SistemaPrimariaDB (DbContextOptions<SistemaPrimariaDB> options)
            : base(options)
        {
        }

        public DbSet<SistemaPrimaria.Models.Materia> Materia { get; set; }

        public DbSet<SistemaPrimaria.Models.Estudiante> Estudiante { get; set; }

        public DbSet<SistemaPrimaria.Models.Maestro> Maestro { get; set; }
    }
}
