using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class Classificacoes
    {

        /// <summary>
        /// Identificador da tabela de classificações
        /// </summary>
        [Key]
        public int Id_TabelaDeClassificacoes { get; set; }

        /// <summary>
        /// Identificador do campeonato correspondente
        /// </summary>
        public Campeonato Campeonato_Id { get; set; }

        /// <summary>
        /// Identificador do clube
        /// </summary>
        public Clube Clube { get; set; }

        /// <summary>
        /// Identificador dos pontos do clube
        /// </summary>
        public int Pontos { get; set; }

        /// <summary>
        /// Identificador dos pontos do clube
        /// </summary>
        public int Golos_Marcados { get; set; }

        /// <summary>
        /// Identificador dos pontos do clube
        /// </summary>
        public int Golos_Sofridos { get; set; }

    }
}
