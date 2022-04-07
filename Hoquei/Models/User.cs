using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hoquei.Models
{
    public class User
    {
        public User()
        {

        }

        /// <summary>
        /// Identificador do utilizador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome real do utilizador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Nome do utilizador na plataforma
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Número de telemóvel do utilizador
        /// </summary>
        public string NumTele { get; set; }

        /// <summary>
        /// Número de cartão de cidadão do utilizador
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        public DateTime DataNascimento { get; set; }
    }
}