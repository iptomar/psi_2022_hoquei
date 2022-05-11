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
    public class Clube
    {
        public Clube()
            {
                ListaDeJogadores = new HashSet<Jogador>();
            }

        /// <summary>
        /// Identificador do jogador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Jogador
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        [Required]
        public DateTime Data_Fundacao { get; set; }

        /// <summary>
        /// Foto do clube
        /// </summary>
        public string FotografiasID { get; set; }

        //*********************************************************

        // um clube está associado a 5 jogadores
        // um clube tem uma lista de jogadores
        /// <summary>
        /// Lista dos jogadores que são da clube
        /// </summary>
        public ICollection<Jogador> ListaDeJogadores { get; set; }
    }
}
