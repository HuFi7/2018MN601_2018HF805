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
    public class pedidosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public pedidosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        ///<summary>
        ///Metodo para tenornar todos los registros de la tabla Pedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("api/pedidos")]
        public IActionResult Get()
        {
            IEnumerable<pedidos> pedidosList = from e in _contexto.pedidos
                                               select e;
            if (pedidosList.Count() > 0)
            {
                return Ok(pedidosList);
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
        [Route("api/pedidos/{id}")]
        public IActionResult getbyId(int id)
        {
            pedidos unPedido = (from e in _contexto.pedidos
                                where e.id == id //filtro por ID
                                select e).FirstOrDefault();
            if (unPedido != null)
            {
                return Ok(unPedido);
            }

            return NotFound();
        }

        ///<summary>
        ///Metodo para retormar un registro nuevo para la tabla Pedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("api/Pedidos")]
        public IActionResult guardarPedido([FromBody] pedidos pedidosNuevo)
        {
            try
            {
                IEnumerable<pedidos> pedidoExiste = from e in _contexto.pedidos
                                                           where e.fecha_pedido == pedidosNuevo.fecha_pedido
                                                           select e;
                if (pedidoExiste.Count() == 0)
                {
                    _contexto.pedidos.Add(pedidosNuevo);
                    _contexto.SaveChanges();
                    return Ok(pedidosNuevo);
                }
                return BadRequest(pedidoExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///Metodo para modifcar un registro de la tabla Pedidos
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPut]
        [Route("api/pedidos")]
        public IActionResult updatePedidos([FromBody] pedidos pedidoModificar, int id)
        {
            pedidos pedidosExiste = (from e in _contexto.pedidos
                                    where e.id == pedidoModificar.id
                                    select e).FirstOrDefault();
            if (pedidosExiste is null)
            {
                return NotFound();
            }

            pedidosExiste.id_cliente = pedidoModificar.id_cliente;
            pedidosExiste.fecha_pedido = pedidoModificar.fecha_pedido;

            _contexto.Entry(pedidosExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(pedidosExiste);
        }
    }
}

