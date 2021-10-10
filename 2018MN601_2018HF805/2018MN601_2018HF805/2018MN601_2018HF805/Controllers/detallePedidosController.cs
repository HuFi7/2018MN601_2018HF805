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
    //[Route("api/[controller]")]
    [ApiController]
    public class detallePedidosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public detallePedidosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary> 
        /// Metodo para retornarr todos los registros de la tabla DetallePedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("api/detallePedidos")]
        public IActionResult Get()
        {
            IEnumerable<detallePedidos> detallePedidosList = from e in _contexto.detallePedidos
                                                             select e;
            if (detallePedidosList.Count() > 0)
            {
                return Ok(detallePedidosList);
            }
            return NotFound();
        }

        ///<summary>
        ///Metodo para retomar un registro de la tabla Pedidos por Id
        /// </summary>
        /// <param name="id">Valor Entero del campo</param>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("api/detallePedidos/{id}")]
        public IActionResult getbyId(int id)
        {
            detallePedidos unDetallePedido = (from e in _contexto.detallePedidos
                                where e.id == id //filtro por ID
                                select e).FirstOrDefault();
            if (unDetallePedido != null)
            {
                return Ok(unDetallePedido);
            }

            return NotFound();
        }

        ///<summary>
        ///Metodo para retormar un registro nuevo para la tabla DetallePedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("api/detallePedidos")]
        public IActionResult guardarDetallePedido([FromBody] detallePedidos detallePedidosNuevo)
        {
            try
            {
                IEnumerable<detallePedidos> equipoExiste = from e in _contexto.detallePedidos
                                                           where e.cantidad == detallePedidosNuevo.cantidad
                                                    select e;
                if (equipoExiste.Count() == 0)
                {
                    _contexto.detallePedidos.Add(detallePedidosNuevo);
                    _contexto.SaveChanges();
                    return Ok(detallePedidosNuevo);
                }
                return BadRequest(equipoExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///Metodo para modifcar un registro de la tabla DetallePedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPut]
        [Route("api/detallepedidos")]
        public IActionResult updateDetallePedidos([FromBody] detallePedidos detallePedidosModificar, int id)
        {
            detallePedidos equipoExiste = (from e in _contexto.detallePedidos
                                     where e.id == detallePedidosModificar.id
                                     select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                return NotFound();
            }

            equipoExiste.id_pedido = detallePedidosModificar.id_pedido;
            equipoExiste.id_producto = detallePedidosModificar.id_producto;
            equipoExiste.cantidad = detallePedidosModificar.cantidad;


            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(equipoExiste);
        }
    }
}
