using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaPrimaria.Models
{
    public class Estudiante
    {
        public Estudiante()
        {
        }
        
        
        public int Id { get; set; }
        
        public string Matricula { get; set; }
        
        public string Nombre { get; set; }
        
        public string ApellidoMaterno { get; set; }
        
        public string ApellidoPaterno { get; set; }
        
        public string Telefono { get; set; }
        
        public string Direccion { get; set; }
        



    }
}
