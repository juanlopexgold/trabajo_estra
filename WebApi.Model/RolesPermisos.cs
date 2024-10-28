using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class RolesPermisos
    {
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }
        public Roles Rol { get; set; }
        public Permisos Permiso { get; set; }
    }
}