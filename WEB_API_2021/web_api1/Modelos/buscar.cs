using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace web_api1.Modelos
{
    public class buscar : Controller
    {
        public buscar()
        {
            dbConexion = Conectar.Create();
        }

        private Conexion dbConexion;

        [HttpGet]
        public ActionResult Get()
        {
            var result =
                (
                from n in dbConexion.Puestos
                join c in dbConexion.Empleados on n.idpuestos equals c.id_puesto
                select
                new {
                    c.idempleado,
                    n.puesto,
                    c.nombres,
                    c.apellidos,
                    c.direccion,
                    c.telefono,
                    c.Fecha_nacimiento
                }
                ).ToList();
            return Ok(result);
        }
    }
}
