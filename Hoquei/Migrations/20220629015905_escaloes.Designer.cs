﻿// <auto-generated />
using System;
using Hoquei.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hoquei.Migrations
{
    [DbContext(typeof(HoqueiDB))]
    [Migration("20220629015905_escaloes")]
    partial class escaloes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClubeJogador", b =>
                {
                    b.Property<int>("ListaDeClubesId")
                        .HasColumnType("int");

                    b.Property<int>("ListaDeJogadoresNum_Fed")
                        .HasColumnType("int");

                    b.HasKey("ListaDeClubesId", "ListaDeJogadoresNum_Fed");

                    b.HasIndex("ListaDeJogadoresNum_Fed");

                    b.ToTable("ClubeJogador");
                });

            modelBuilder.Entity("Hoquei.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UtilizadorFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Hoquei.Models.Campeonato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Designacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("escalaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("escalaoId");

                    b.ToTable("Campeonato");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Designacao = "SuperLiga"
                        });
                });

            modelBuilder.Entity("Hoquei.Models.Classificacoes", b =>
                {
                    b.Property<int>("Id_TabelaDeClassificacoes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Campeonato_IdId")
                        .HasColumnType("int");

                    b.Property<int?>("ClubeId")
                        .HasColumnType("int");

                    b.Property<int>("Golos_Marcados")
                        .HasColumnType("int");

                    b.Property<int>("Golos_Sofridos")
                        .HasColumnType("int");

                    b.Property<int>("Pontos")
                        .HasColumnType("int");

                    b.HasKey("Id_TabelaDeClassificacoes");

                    b.HasIndex("Campeonato_IdId");

                    b.HasIndex("ClubeId");

                    b.ToTable("Classificacoes");
                });

            modelBuilder.Entity("Hoquei.Models.Clube", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampeonatoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data_Fundacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("FotoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("FotoId")
                        .IsUnique();

                    b.ToTable("Clube");
                });

            modelBuilder.Entity("Hoquei.Models.Escalao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("designacao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Escalao");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            designacao = "Infantis"
                        },
                        new
                        {
                            Id = 2,
                            designacao = "Iniciados"
                        },
                        new
                        {
                            Id = 3,
                            designacao = "Juvenis"
                        },
                        new
                        {
                            Id = 4,
                            designacao = "Juniores"
                        },
                        new
                        {
                            Id = 5,
                            designacao = "Seniores"
                        });
                });

            modelBuilder.Entity("Hoquei.Models.Fotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Foto");
                });

            modelBuilder.Entity("Hoquei.Models.Jogador", b =>
                {
                    b.Property<int>("Num_Fed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alcunha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data_Nasc")
                        .HasColumnType("datetime2");

                    b.Property<int>("FotoId")
                        .HasColumnType("int");

                    b.Property<int?>("JogoId")
                        .HasColumnType("int");

                    b.Property<int?>("JogoId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num_Cam")
                        .HasColumnType("int");

                    b.Property<int>("Numero_FederadoReal")
                        .HasColumnType("int");

                    b.HasKey("Num_Fed");

                    b.HasIndex("FotoId")
                        .IsUnique();

                    b.HasIndex("JogoId");

                    b.HasIndex("JogoId1");

                    b.ToTable("Jogador");
                });

            modelBuilder.Entity("Hoquei.Models.Jogo", b =>
                {
                    b.Property<int>("JogoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampeonatosId")
                        .HasColumnType("int");

                    b.Property<int?>("Capitao_CasaNum_Fed")
                        .HasColumnType("int");

                    b.Property<int?>("Capitao_ForaNum_Fed")
                        .HasColumnType("int");

                    b.Property<int?>("Clube_CasaId")
                        .HasColumnType("int");

                    b.Property<int?>("Clube_ForaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("GolosCasa")
                        .HasColumnType("int");

                    b.Property<int>("GolosFora")
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JogoId");

                    b.HasIndex("CampeonatosId");

                    b.HasIndex("Capitao_CasaNum_Fed");

                    b.HasIndex("Capitao_ForaNum_Fed");

                    b.HasIndex("Clube_CasaId");

                    b.HasIndex("Clube_ForaId");

                    b.ToTable("Jogo");
                });

            modelBuilder.Entity("Hoquei.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("NumTele")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNameId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "u",
                            ConcurrencyStamp = "4e50dc3a-28ff-4f66-80da-26d82e321981",
                            Name = "Utilizador",
                            NormalizedName = "UTILIZADOR"
                        },
                        new
                        {
                            Id = "a",
                            ConcurrencyStamp = "02c28b60-4c6e-4eed-b119-d09a6b22ac7c",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ClubeJogador", b =>
                {
                    b.HasOne("Hoquei.Models.Clube", null)
                        .WithMany()
                        .HasForeignKey("ListaDeClubesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hoquei.Models.Jogador", null)
                        .WithMany()
                        .HasForeignKey("ListaDeJogadoresNum_Fed")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hoquei.Models.Campeonato", b =>
                {
                    b.HasOne("Hoquei.Models.Escalao", "escalao")
                        .WithMany()
                        .HasForeignKey("escalaoId");

                    b.Navigation("escalao");
                });

            modelBuilder.Entity("Hoquei.Models.Classificacoes", b =>
                {
                    b.HasOne("Hoquei.Models.Campeonato", "Campeonato_Id")
                        .WithMany("ListaDeClassificacoes")
                        .HasForeignKey("Campeonato_IdId");

                    b.HasOne("Hoquei.Models.Clube", "Clube")
                        .WithMany()
                        .HasForeignKey("ClubeId");

                    b.Navigation("Campeonato_Id");

                    b.Navigation("Clube");
                });

            modelBuilder.Entity("Hoquei.Models.Clube", b =>
                {
                    b.HasOne("Hoquei.Models.Campeonato", null)
                        .WithMany("ListaDeClubes")
                        .HasForeignKey("CampeonatoId");

                    b.HasOne("Hoquei.Models.Fotos", "Foto")
                        .WithOne("Club")
                        .HasForeignKey("Hoquei.Models.Clube", "FotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Foto");
                });

            modelBuilder.Entity("Hoquei.Models.Jogador", b =>
                {
                    b.HasOne("Hoquei.Models.Fotos", "Foto")
                        .WithOne("Player")
                        .HasForeignKey("Hoquei.Models.Jogador", "FotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hoquei.Models.Jogo", null)
                        .WithMany("ListaDeMarcadoresCasa")
                        .HasForeignKey("JogoId");

                    b.HasOne("Hoquei.Models.Jogo", null)
                        .WithMany("ListaDeMarcadoresFora")
                        .HasForeignKey("JogoId1");

                    b.Navigation("Foto");
                });

            modelBuilder.Entity("Hoquei.Models.Jogo", b =>
                {
                    b.HasOne("Hoquei.Models.Campeonato", "Campeonatos")
                        .WithMany("ListaDeJogos")
                        .HasForeignKey("CampeonatosId");

                    b.HasOne("Hoquei.Models.Jogador", "Capitao_Casa")
                        .WithMany()
                        .HasForeignKey("Capitao_CasaNum_Fed");

                    b.HasOne("Hoquei.Models.Jogador", "Capitao_Fora")
                        .WithMany()
                        .HasForeignKey("Capitao_ForaNum_Fed");

                    b.HasOne("Hoquei.Models.Clube", "Clube_Casa")
                        .WithMany()
                        .HasForeignKey("Clube_CasaId");

                    b.HasOne("Hoquei.Models.Clube", "Clube_Fora")
                        .WithMany()
                        .HasForeignKey("Clube_ForaId");

                    b.Navigation("Campeonatos");

                    b.Navigation("Capitao_Casa");

                    b.Navigation("Capitao_Fora");

                    b.Navigation("Clube_Casa");

                    b.Navigation("Clube_Fora");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Hoquei.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Hoquei.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hoquei.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Hoquei.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hoquei.Models.Campeonato", b =>
                {
                    b.Navigation("ListaDeClassificacoes");

                    b.Navigation("ListaDeClubes");

                    b.Navigation("ListaDeJogos");
                });

            modelBuilder.Entity("Hoquei.Models.Fotos", b =>
                {
                    b.Navigation("Club");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Hoquei.Models.Jogo", b =>
                {
                    b.Navigation("ListaDeMarcadoresCasa");

                    b.Navigation("ListaDeMarcadoresFora");
                });
#pragma warning restore 612, 618
        }
    }
}