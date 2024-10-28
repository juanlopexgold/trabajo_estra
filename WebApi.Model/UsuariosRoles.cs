using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class UsuariosRoles
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public Usuarios Usuario { get; set; }
        public Roles Rol { get; set; }
    }
}