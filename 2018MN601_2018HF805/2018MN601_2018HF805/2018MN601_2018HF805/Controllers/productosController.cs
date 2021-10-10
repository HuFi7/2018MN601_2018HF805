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
    public class productosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public productosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/productos")]

        public IActionResult Get()
        {
            IEnumerable<productos> productosList = from e in _contexto.productos
                                                   select e;
            if (productosList.Count() > 0)
            {
                return Ok(productosList);
            }
            return NotFound();
        }



        /// <summary>
        /// Metodo para retornar los reg. de la tabla productos que contenga el valor dado en el parametro.
        /// </summary>
        /// <param id="buscarId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/productos/{id}")]
        public IActionResult getbyId(int id)
        {
            productos unProducto = (from e in _contexto.productos
                                    where e.Id == id
                                    select e).FirstOrDefault();
            if (unProducto != null)
            {
                return Ok(unProducto);
            }

            return NotFound();
        }


        [HttpPost]
        [Route("api/productos")]
        public IActionResult guardarContains([FromBody] productos productoNuevo)
        {
            try
            {
                IEnumerable<productos> productoExiste = from e in _contexto.productos
                                                        where e.Producto == productoNuevo.Producto
                                                        select e;
                if (productoExiste.Count() == 0)
                {
                    _contexto.productos.Add(productoNuevo);
                    _contexto.SaveChanges();
                    return Ok(productoNuevo);
                }
                return BadRequest(productoExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/productos")]
        public IActionResult updateProducto([FromBody] productos productoModificar, int id)
        {
            productos productoExiste = (from e in _contexto.productos
                                        where e.Id == productoModificar.Id
                                        select e).FirstOrDefault();
            if (productoExiste is null)
            {
                return NotFound();
            }

            productoExiste.Id = productoModificar.Id;
            productoExiste.Producto = productoModificar.Producto;
            productoExiste.Precio = productoModificar.Precio;


            _contexto.Entry(productoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(productoExiste);
        }

    }
}