using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api1.Modelos;

namespace web_api1.Controllers
{
    [Route("api/[controller]")]
    public class PuestosController : Controller
    {
        private Conexion dbConexion;

        public PuestosController()
        {
            dbConexion = Conectar.Create();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(dbConexion.Puestos.ToArray());
        }

        // seleccionar uno
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var puestos =
                dbConexion.Puestos.SingleOrDefault(a => a.idpuestos == id);
            if (puestos != null)
            {
                return Ok(puestos);
            }
            else
            {
                return NotFound();
            }
        }

        ///////Insertar datos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Puestos puestos)
        {
            if (ModelState.IsValid)
            {
                dbConexion.Puestos.Add (puestos);
                await dbConexion.SaveChangesAsync();
                return Ok(puestos); //retorna el registro ingresado
                //return Created("api/clientes",clientes); retorna los registros
            }
            else
            {
                return BadRequest();
            }
        }

        //Modificar datos
        public async Task<ActionResult> Put([FromBody] Puestos puestos)
        {
            var v_puestos =
                dbConexion
                    .Puestos
                    .SingleOrDefault(a => a.idpuestos == puestos.idpuestos);
            if (v_puestos != null && ModelState.IsValid)
            {
                dbConexion.Entry(v_puestos).CurrentValues.SetValues(puestos);
                await dbConexion.SaveChangesAsync();
            //  return Created("api/puestos",puestos);
              return Ok(v_puestos);
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
            var puestos =
                dbConexion.Puestos.SingleOrDefault(a => a.idpuestos == id);
            if (puestos != null)
            {
                dbConexion.Puestos.Remove (puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
