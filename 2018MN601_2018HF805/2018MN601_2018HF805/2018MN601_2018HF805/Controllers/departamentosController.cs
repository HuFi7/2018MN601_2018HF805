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
    public class departamentosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public departamentosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/departamentos")]

        public IActionResult Get()
        {
            IEnumerable<departamentos> departamentosList = from e in _contexto.departamentos
                                                           select e;
            if (departamentosList.Count() > 0)
            {
                return Ok(departamentosList);
            }
            return NotFound();
        }


        /// <summary>
        /// Metodo para retornar los reg. de la tabla departamentos que contenga el valor dado en el parametro.
        /// </summary>
        /// <param id="buscarId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/departamentos/{id}")]
        public IActionResult GetbyId(int id)
        {
            departamentos unDepartamento = (from e in _contexto.departamentos
                                            where e.Id == id //filtro por ID
                                            select e).FirstOrDefault();
            if (unDepartamento != null)
            {
                return Ok(unDepartamento);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("api/departamentos")]
        public IActionResult guardarContains([FromBody] departamentos departamentoNuevo)
        {
            try
            {
                IEnumerable<departamentos> departamentoExiste = from e in _contexto.departamentos
                                                                where e.departamento == departamentoNuevo.departamento
                                                                select e;
                if (departamentoExiste.Count() == 0)
                {
                    _contexto.departamentos.Add(departamentoNuevo);
                    _contexto.SaveChanges();
                    return Ok(departamentoNuevo);
                }
                return BadRequest(departamentoExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/departamentos")]
        public IActionResult updateDepartamento([FromBody] departamentos departamentoModificar, int id)
        {
            departamentos departamentoExiste = (from e in _contexto.departamentos
                                                where e.Id == departamentoModificar.Id
                                                select e).FirstOrDefault();
            if (departamentoExiste is null)
            {
                return NotFound();
            }

            departamentoExiste.Id = departamentoModificar.Id;
            departamentoExiste.departamento = departamentoModificar.departamento;


            _contexto.Entry(departamentoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(departamentoExiste);
        }

    }
}