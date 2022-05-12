using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class Fotos
    {
        /// <summary>
        /// Id da Foto
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do ficheiro com a fotografia do hotel
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Path para a foto
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Descricao da foto
        /// </summary>
        public string Descricao { get; set; }


        /// <summary>
        /// Identifica o jogador a que a foto pertence
        /// </summary>        
        public Jogador Player { get; set; }
        
    }
}
