using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2018MN601_2018HF805.Models
{

    public class departamentos
    {
        [Key]
        public int Id { get; set; }

        public string departamento { get; set; }
    }
}