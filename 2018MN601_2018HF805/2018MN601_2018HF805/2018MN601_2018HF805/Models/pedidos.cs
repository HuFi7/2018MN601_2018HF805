using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2018MN601_2018HF805.Models
{
    public class pedidos
    {
        [Key]
        public int id { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha_pedido { get; set; }
    }
}
