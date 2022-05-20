using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class Escalao
    {
        [Key]
        public int Id { get; set; }

        public string  designacao { get; set; }
    }
}
