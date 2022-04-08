using Hoquei.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hoquei.Data
{

    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// recolhe a data de registo de um utilizador
        /// </summary>
        public DateTime DataRegisto { get; set; }

        ////[ForeignKey(nameof(cliente))]
        ////public int UtilizadorFK { get; set; }
        //public virtual User user { get; set; }
    }
    /// <summary>
    /// classe para recolher os dados particulares dos Utilizadores
    /// vamos deixar de usar o 'IdentityUser' e começar a usar este
    /// A adição desta classe implica:
    ///    - mudar a classe de criação da Base de Dados
    ///    - mudar no ficheiro 'startup.cs' a referência ao tipo do utilizador
    ///    - mudar em todos os ficheiros do projeto a referência a 'IdentityUser' 
    ///           para 'ApplicationUser'
    /// </summary>
    public class HoqueiDB : IdentityDbContext<ApplicationUser>
    {
        



        public HoqueiDB(DbContextOptions<HoqueiDB> options) : base(options)
        { }
            protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = "u", Name = "Utilizador", NormalizedName = "UTILIZADOR" },
             new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
             );
        }
           
        public DbSet<User> User { get; set; }
    }
            
        
    }


