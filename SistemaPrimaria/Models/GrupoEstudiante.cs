using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPrimaria.Models
{
    public class GrupoEstudiante
    {

        public GrupoEstudiante()
        {

        }

        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public int IdEstudiante { get; set; }

    }
}
