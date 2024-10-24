using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }
        public Rol Rol { get; set; }
    }
}