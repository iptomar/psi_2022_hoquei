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

            modelbuilder.Entity<Escalao>().HasData(
                new Escalao { Id = 1, designacao = "Infantis" },
                new Escalao { Id = 2, designacao = "Iniciados" },
                new Escalao { Id = 3, designacao = "Juvenis" },
                new Escalao { Id = 4, designacao = "Juniores" },
                new Escalao { Id = 5, designacao = "Seniores" }
                );
            //modelbuilder.Entity<Jogador>().HasOne(j => j.Foto).WithOne(f => f.).HasForeignKey<Fotos>(b => b.JogadorFK);


            modelbuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = "u", Name = "Utilizador", NormalizedName = "UTILIZADOR" },
             new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
             );
            modelbuilder.Entity<Campeonato>().HasData(
               new Campeonato { Id = 1, Designacao = "SuperLiga"}
                );


        }
           
        public DbSet<User> User { get; set; }
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Campeonato> Campeonato { get; set; }
        public DbSet<Escalao> Escalao { get; set; }
        public DbSet<Fotos> Foto { get; set; }
        public DbSet<Jogador> ListaDeJogadores{ get; set; }
        public DbSet<Clube> Clube { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Classificacoes> Classificacoes { get; set; }
    }
            
        
    }


