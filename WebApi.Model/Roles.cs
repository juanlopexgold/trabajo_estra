using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Roles
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<UsuariosRoles> UsuariosRoles { get; set; }
        public ICollection<RolesPermisos> RolesPermisos { get; set; }
    }
}