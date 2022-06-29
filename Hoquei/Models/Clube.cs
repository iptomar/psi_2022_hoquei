using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{


    /// <summary>
    /// Descrição de um Clube
    /// </summary>
    /// 
    
    public class Clube
    {
        public Clube()
        {
            ListaDeJogadores = new HashSet<Jogador>();
        }
        /// <summary>
        /// Identificador do Clube
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Clube
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Data de fundação do Clube
        /// </summary>
        [Required]
        public DateTime Data_Fundacao { get; set; }

        ///// <summary>
        ///// Foto do carro
        ///// </summary>
        //public string Foto { get; set; }

        // criação da FK que referencia as fotos ao Clube a que pertencem 
        [ForeignKey(nameof(Fotos))]
        public int FotoId { get; set; }
        public Fotos Foto { get; set; }


        public ICollection<Jogador> ListaDeJogadores { get; set; }
    }
}
