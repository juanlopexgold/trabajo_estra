using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public DateTime FechaLlegada { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public string Comentario { get; set; }
    }
}