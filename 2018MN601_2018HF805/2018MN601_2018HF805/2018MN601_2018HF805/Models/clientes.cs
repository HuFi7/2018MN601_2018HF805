using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2018MN601_2018HF805.Models
{
    public class clientes
    {
        public int Id { get; set; }

        public int IdDepartamento { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaNac { get; set; }
    }
}