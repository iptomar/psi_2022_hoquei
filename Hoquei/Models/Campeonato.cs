using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class Campeonato
    {
        
        [Key]
        public int Id { get; set; }
 
        public string Designacao { get; set; }
   
        public Escalao escalao { get; set; }
        
    }
}
