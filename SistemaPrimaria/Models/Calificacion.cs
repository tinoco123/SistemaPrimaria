using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPrimaria.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int calificacion { get; set; }

        public int IdEstudiante { get; set; }
        public int IdMateria { get; set; }
    }
}
