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

        // <summary>
        // recolhe a data de registo de um utilizador
        // </summary>
        public DateTime DataRegisto { get; set; }

        [ForeignKey(nameof(User.Id))]
        public int UtilizadorFK { get; set; }
        
        //public virtual User User { get; set; }
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

            //modelbuilder.Entity<IdentityRole>().HasData(
            // new IdentityRole { Id = "u", Name = "Utilizador", NormalizedName = "UTILIZADOR" },
            // new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
            // );

          //  modelbuilder.Entity<User>().HasData(
          //   new User { Id = 1, Nome = "Marisa Vieira", UserName = "MarVi", Email = "Marisa.Freitas@iol.pt", NumTele = "967197885", CC = "098446793", DataNascimento = new DateTime(2019, 4, 16) },
          //   new User { Id = 2, Nome = "Fátima Ribeiro", UserName = "FáRibeiro", Email = "Fátima.Machado@gmail.com", NumTele = "963737476", CC = "098446795", DataNascimento = new DateTime(2019, 10, 10) },
          //   new User { Id = 4, Nome = "Paula Silva", UserName = "Pauva", Email = "Paula.Lopes@iol.pt", NumTele = "967517256", CC = "098446801", DataNascimento = new DateTime(2011, 3, 22) },
          //   new User { Id = 5, Nome = "Mariline Marques", UserName = "Mariques", Email = "Mariline.Martins@sapo.pt", NumTele = "967212144", CC = "098446804", DataNascimento = new DateTime(2008, 6, 8) },
          //   new User { Id = 6, Nome = "Marcos Rocha", UserName = "Marcha", Email = "Marcos.Rocha@sapo.pt", NumTele = "962125638", CC = "098446807", DataNascimento = new DateTime(2012, 8, 21) },
          //   new User { Id = 7, Nome = "Alexandre Vieira", UserName = "Alexeira", Email = "Alexandre.Dias@hotmail.com", NumTele = "961493756", CC = "098446809", DataNascimento = new DateTime(2010, 10, 1) },
          //   new User { Id = 8, Nome = "Paula Soares", UserName = "Paulares", Email = "Paula.Vieira@clix.pt", NumTele = "961937768", CC = "098446811", DataNascimento = new DateTime(2010, 12, 11) },
          //   new User { Id = 9, Nome = "Mariline Santos", UserName = "Marilintos", Email = "Mariline.Ribeiro@iol.pt", NumTele = "964106478", CC = "098446799", DataNascimento = new DateTime(2017, 3, 21) },
          //   new User { Id = 10, Nome = "Roberto Pinto", UserName = "RoPinto", Email = "Roberto.Vieira@sapo.pt", NumTele = "964685937", CC = "098446812", DataNascimento = new DateTime(2018, 1, 4) }
          //);
        }
           
        public DbSet<User> User { get; set; }
    }
            
        
    }


