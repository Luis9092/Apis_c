using System.ComponentModel.DataAnnotations;

namespace web_api1.Modelos{
public class Clientes{
        [Key]
        public int id_cliente { get; set; }
        public string nit { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string Fecha_nacimiento { get; set; }
        
}

}