using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api1.Modelos;

namespace web_api1.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadosController : Controller
    {
        public EmpleadosController()
        {
            dbConexion = Conectar.Create();
        }

        private Conexion dbConexion;

        [HttpGet]
        public ActionResult Get()
        {
            var result =
                (
                from n in dbConexion.Puestos //Donde se  saca el puesto
                join c in dbConexion.Empleados on n.idpuestos equals c.id_puesto //Se compara el puesto con empleados
                select //Se selecciona
                new {
                    c.idempleado,
                    c.codigo,
                    n.puesto, // PUESTO
                    c.nombres,
                    c.apellidos,
                    c.direccion,
                    c.telefono,
                    c.Fecha_nacimiento
                }
                ).ToList(); // Se añade a una lista

            return Ok(result); // Se muestra el resultado
        }

        // seleccionar uno
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var empleados =
                dbConexion.Empleados.SingleOrDefault(a => a.idempleado == id);

            if (empleados != null)
            {
                var buscar =
                    (
                    from n in dbConexion.Puestos
                    join c
                    in dbConexion.Empleados
                    on n.idpuestos
                    equals c.id_puesto
                    where c.idempleado == id
                    select
                    new {
                        c.idempleado,
                        c.codigo,
                        n.puesto,
                        c.nombres,
                        c.apellidos,
                        c.direccion,
                        c.telefono,
                        c.Fecha_nacimiento
                    }
                    );

                return Ok(buscar);
            }
            else
            {
                return NotFound();
            }
        }

        ///////Insertar datos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                dbConexion.Empleados.Add (empleados);
                await dbConexion.SaveChangesAsync();
                return Ok(empleados); //retorna el registro ingresado
                //return Created("api/clientes",clientes); retorna los registros
            }
            else
            {
                return BadRequest();
            }
        }

        //Modificar datos
        public async Task<ActionResult> Put([FromBody] Empleados empleados)
        {
            var v_empleados =
                dbConexion
                    .Empleados
                    .SingleOrDefault(a => a.idempleado == empleados.idempleado);
            if (v_empleados != null && ModelState.IsValid)
            {
                dbConexion
                    .Entry(v_empleados)
                    .CurrentValues
                    .SetValues(empleados);
                await dbConexion.SaveChangesAsync();

                return Ok(v_empleados);
            }
            else
            {
                return BadRequest();
            }
        }

        /// Eliminar datos
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empleados =
                dbConexion.Empleados.SingleOrDefault(a => a.idempleado == id);
            if (empleados != null)
            {
                dbConexion.Empleados.Remove (empleados);
                await dbConexion.SaveChangesAsync();
                return Ok("Ha Sido Eliminado Perro :vv");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
