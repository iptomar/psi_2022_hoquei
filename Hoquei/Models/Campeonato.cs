using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class Campeonato
    {
        public Campeonato()
        {
            ListaDeJogos = new HashSet<Jogo>();
            ListaDeClassificacoes = new HashSet<Classificacoes>();
            ListaDeClubes = new HashSet<Clube>();
        }

        [Key]
        public int Id { get; set; }
 
        /// <summary>
        /// Nome do campeonato
        /// </summary>

        /// <summary>
        /// Nome do campeonato
        /// </summary>
        public string Designacao { get; set; }
        /// <summary>
        /// Referência para o escalão a que o campeonato pertence
        /// </summary>
        //public Escalao escalao { get; set; }
        /// <summary>
        /// Lista de jogos presentes no jogo
        /// </summary>
        public ICollection<Jogo> ListaDeJogos { get; set; }
        /// <summary>
        /// Lista de calssificações do campeonato
        /// </summary>
        public ICollection<Classificacoes> ListaDeClassificacoes { get; set; }

        /// <summary>
        /// Lista de Clubes do campeonato
        /// </summary>
        public ICollection<Clube> ListaDeClubes { get; set; }
    }
}
