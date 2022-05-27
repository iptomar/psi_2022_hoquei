using Microsoft.AspNetCore.Http;
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
        public Jogador()
        {
            // inicializar a lista de Clubes do Jogador
            ListaDeClubes = new HashSet<Clube>();
        }

        /// <summary>
        /// Identificador Federativo do jogador
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
        public DateTime Data_Nasc { get; set; }

        /// <summary>
        /// Alcunha
        /// </summary>
        [Required]
        public string Alcunha { get; set; }



        // criação da FK que referencia as fotos ao Jogador a que pertencem 
        [ForeignKey(nameof(Fotos))]
        public int FotoId { get; set; }
        public Fotos Foto { get; set; }
        // NOTA: O nome dos models deviam estar no plural para evitar atrofios na nomenclatura.
        // ex: Agora como o nome do model é Fotos já posso fazer public Fotos Foto
        // Só não alterei nos outros para não haver confusões depois no momento em que alguém faça pull ou merge no GIT
        // ass: Gonçalo

        /*
        /// <summary>
        /// Foto do jogador
        /// </summary>
        public string Foto { get; set; }

        /// <summary>

    }
}
