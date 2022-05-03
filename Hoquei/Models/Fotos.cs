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


        // criação da FK que referencia as fotos ao Jogador a que pertencem 
        [ForeignKey(nameof(Jogador))]
        public int HotelFK { get; set; }
        public Jogador Player { get; set; }
        // NOTA: O nome dos models deviam estar no plural para evitar atrofios na nomenclatura.
        // ex: Agora se o nome do model fosse Jogadores já podia fazer public Jogadores Jogador
        // Só não alterei para não fazer confusões depois no momento em que alguém faça pull ou merge no GIT
        // ass: Gonçalo
    }
}
