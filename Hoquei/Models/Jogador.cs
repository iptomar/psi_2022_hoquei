using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    /// <summary>
    /// Descrição de um Jogador
    /// </summary>
    public class Jogador
    {
        /// <summary>
        /// Identificador do jogador
        /// </summary>
        [Key]
        public int Num_Fed { get; set; }

        /// <summary>
        /// Nome do Jogador
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Numero da camisola
        /// </summary>
       [Required]
        public int Num_Cam { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        [Required]
        public string Data_Nasc { get; set; }

        /// <summary>
        /// Alcunha
        /// </summary>
        [Required]
        public string Alcunha { get; set; }
    }
}
