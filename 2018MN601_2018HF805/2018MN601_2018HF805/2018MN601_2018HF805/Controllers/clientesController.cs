using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2018MN601_2018HF805.Models;
using Microsoft.EntityFrameworkCore;

namespace _2018MN601_2018HF805.Controllers
{
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public clientesController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/clientes")]

        public IActionResult Get()
        {
            IEnumerable<clientes> clientesList = from e in _contexto.clientes
                                                 select e;
            if (clientesList.Count() > 0)
            {
                return Ok(clientesList);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para retornar los reg. de la tabla clientes que contenga el valor dado en el parametro.
        /// </summary>
        /// <param id="buscarId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/clientes/{id}")]
        public IActionResult GetbyId(int id)
        {
            clientes unCliente = (from e in _contexto.clientes
                                  where e.Id == id //filtro por ID
                                  select e).FirstOrDefault();
            if (unCliente != null)
            {
                return Ok(unCliente);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("api/clientes")]
        public IActionResult guardarContains([FromBody] clientes clienteNuevo)
        {
            try
            {
                IEnumerable<clientes> clienteExiste = from e in _contexto.clientes
                                                      where e.Nombre == clienteNuevo.Nombre
                                                      select e;
                if (clienteExiste.Count() == 0)
                {
                    _contexto.clientes.Add(clienteNuevo);
                    _contexto.SaveChanges();
                    return Ok(clienteNuevo);
                }
                return BadRequest(clienteExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/clientes")]
        public IActionResult updateCliente([FromBody] clientes clientesModificar, int id)
        {
            clientes clienteExiste = (from e in _contexto.clientes
                                      where e.Id == clientesModificar.Id
                                      select e).FirstOrDefault();
            if (clienteExiste is null)
            {
                return NotFound();
            }

            clienteExiste.Id = clientesModificar.Id;
            clienteExiste.IdDepartamento = clientesModificar.IdDepartamento;
            clienteExiste.Nombre = clientesModificar.Nombre;
            clienteExiste.FechaNac = clientesModificar.FechaNac;


            _contexto.Entry(clienteExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(clienteExiste);
        }

    }
}
