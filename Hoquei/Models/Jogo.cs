using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    /// <summary>
    /// Descrição de um Jogo
    /// </summary>
    public class Jogo
    {
        public Jogo()
        { 
            ListaDeMarcadores = new HashSet<Jogador>();
        }
        /// <summary>
        /// Identificador do jogo
        /// </summary>
        [Key]
        public int JogoId { get; set; }

        /// <summary>
        /// Local do jogo
        /// </summary>
        [Required]
        public string Local { get; set; }

        /// <summary>
        /// Data do Jogo
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Data { get; set; }

        /// <summary>
        /// Equipa visitada
        /// </summary>
        [Required]
        public Clube Clube_Casa { get; set; }

        /// <summary>
        /// Equipa visitante
        /// </summary>
        [Required]
        public Clube Clube_Fora { get; set; }

        /// <summary>
        /// Escalão
        /// </summary>
        [Required]
        public string Escalao { get; set; }

        /// <summary>
        /// Golos da equipa visitada
        /// </summary>
        [Required]
        public int GolosCasa { get; set; }

        /// <summary>
        /// Golos da equipa visitante
        /// </summary>
        [Required]
        public int GolosFora { get; set; }

        /// <summary>
        /// Capitao da equipa da casa
        /// </summary>
        [Required]
        public Jogador Capitao_Casa { get; set; }

        /// <summary>
        /// Capitao da equipa da casa
        /// </summary>
        [Required]
        public Jogador Capitao_Fora { get; set; }


        /// <summary>
        /// lista de marcadores
        /// </summary>
        public ICollection<Jogador> ListaDeMarcadores { get; set; }

    }
}
