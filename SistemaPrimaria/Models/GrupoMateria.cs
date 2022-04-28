using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPrimaria.Models
{
    public class GrupoMateria
    {

        public GrupoMateria()
        {

        }

        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public int IdMateria { get; set; }
    }
}
