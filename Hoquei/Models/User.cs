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
        /// [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "O {0} não pode ter mais de {1} caracteres.")]
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Nome do utilizador na plataforma
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Número de telemóvel do utilizador
        /// </summary>
        /// [StringLength(14, MinimumLength = 9, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        [RegularExpression("(00)?([0-9]{2,3})?[1-9][0-9]{8}",
                           ErrorMessage = "Escreva um nº Telemóvel com 9 algarismos. Pode acrescentar o indicativo.")]
        [Display(Name = "Telemóvel")]
        [Required]
        public string NumTele { get; set; }
        
        /// <summary>
        /// Número de cartão de cidadão do utilizador
        /// </summary>
        [Required]
        [RegularExpression("[0-9]{9}",
                           ErrorMessage = "Escreva um nº de cartão válido")]
        [Display(Name = "Nº de cartão de cidadão")]
        public string CC { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Chave de ligação entre a autenticação e os users
        /// </summary>
        public string UserNameId { get; set; }
    }
}