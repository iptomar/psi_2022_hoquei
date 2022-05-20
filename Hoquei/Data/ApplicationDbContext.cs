﻿using Hoquei.Models;
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
                new Escalao { Id = 1, designacao = "infantis" },
                new Escalao { Id = 2, designacao = "iniciados" },
                new Escalao { Id = 3, designacao = "juvenis" },
                new Escalao { Id = 4, designacao = "juniores" },
                new Escalao { Id = 5, designacao = "seniores"}
                );
        }
           
        public DbSet<User> User { get; set; }
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Campeonato> Campeonato { get; set; }
        public DbSet<Escalao> Escalao { get; set; }
    }
            
        
    }


