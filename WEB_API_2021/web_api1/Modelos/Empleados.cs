using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web_api1.Modelos
{
    public class Empleados
    {
        [Key]
        public int idempleado { get; set; }

        public string codigo { get; set; }

        public string nombres { get; set; }

        public string apellidos { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }

        public string Fecha_nacimiento { get; set; }

        public int id_puesto { get; set; }
    }
}
