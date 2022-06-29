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
        /// Nome do ficheiro com a fotografia do Jogador
        /// </summary>
        public string Nome { get; set; }
        // NOTA: recomendo que quando forem tirar prints para as fotos
        // dos jogadores as tirem segundo as seguintes medidas:
        // a altura convém ser 10px maior do que a largura e só deverá
        // ter a cara do moço
        // ASS: Gonçalo

 
        /// <summary>
        /// Descricao da foto
        /// </summary>
        public string Descricao { get; set; }


        /// <summary>
        /// Identifica o jogador a que a foto pertence
        /// </summary>        
        public Jogador Player { get; set; }
        
        // <summary>
        // Identifica o clube a que a foto pertence
        // </summary>        
        public Clube Club { get; set; }

    }
}
