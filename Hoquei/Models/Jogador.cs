﻿using System;
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
        /// Identificador do jogador
        /// </summary>
        [Key]
        public int Num_Fed { get; set; }

        /// <summary>
        /// Nome do Jogador
        /// </summary>
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
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


        /// <summary>
        /// Foto do Jogador
        /// </summary>
        public string Foto { get; set; }

        // ********************************************************

        /// <summary>
        /// Lista de clubes dos jogadores
        /// </summary>
        public ICollection<Clube> ListaDeClubes { get; set; }

    }
}
