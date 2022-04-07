using Hoquei.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoquei.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>().HasData(
                new Jogador { Num_Fed = 1, Name = "Antonio Alberto", Num_Cam = 10, Data_Nasc = "25/12/2000", Alcunha = "Toni"}

            );
        }
        public DbSet<Jogador> Jogador { get; set; }
    }
}
