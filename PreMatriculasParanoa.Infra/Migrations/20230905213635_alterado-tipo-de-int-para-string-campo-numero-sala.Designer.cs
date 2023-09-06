﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PreMatriculasParanoa.Infra.Context;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    [DbContext(typeof(EntityContext))]
    [Migration("20230905213635_alterado-tipo-de-int-para-string-campo-numero-sala")]
    partial class alteradotipodeintparastringcamponumerosala
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Escola", b =>
                {
                    b.Property<int>("IdEscola")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEscola"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModalidadeEnsino")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Regiao")
                        .HasColumnType("int");

                    b.HasKey("IdEscola");

                    b.ToTable("Escolas");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoAnoLetivo", b =>
                {
                    b.Property<int>("IdPlanejamentoAnoLetivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlanejamentoAnoLetivo"));

                    b.Property<int>("AnoLetivo")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataInicioPlanejamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataTerminoPlanejamento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEscola")
                        .HasColumnType("int");

                    b.HasKey("IdPlanejamentoAnoLetivo");

                    b.HasIndex("IdEscola");

                    b.ToTable("PlanejamentosAnosLetivos");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoMatriculaSequencial", b =>
                {
                    b.Property<int>("IdPlanejamentoMatriculaSequencial")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlanejamentoMatriculaSequencial"));

                    b.Property<int>("AnoLetivo")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("IdEscolaDestino")
                        .HasColumnType("int");

                    b.Property<int>("IdEscolaOrigem")
                        .HasColumnType("int");

                    b.Property<int>("PeriodoMatriculaSequencial")
                        .HasColumnType("int");

                    b.Property<int>("TotalMatriculas")
                        .HasColumnType("int");

                    b.HasKey("IdPlanejamentoMatriculaSequencial");

                    b.HasIndex("IdEscolaDestino");

                    b.HasIndex("IdEscolaOrigem");

                    b.ToTable("PlanejamentosMatriculasSequenciais");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoSerieAno", b =>
                {
                    b.Property<int>("IdPlanejamentoSerieAno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlanejamentoSerieAno"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("EntradaAprovadosSerieAnoAnterior")
                        .HasColumnType("int");

                    b.Property<int>("EntradaCentralMatricula")
                        .HasColumnType("int");

                    b.Property<int>("EntradaRemanejamento")
                        .HasColumnType("int");

                    b.Property<int>("EntradaRetidosSerieAnoAtual")
                        .HasColumnType("int");

                    b.Property<int>("EntradaSequencial")
                        .HasColumnType("int");

                    b.Property<int>("IdPlanejamentoAnoLetivo")
                        .HasColumnType("int");

                    b.Property<bool>("PrimeiraSerieAno")
                        .HasColumnType("bit");

                    b.Property<int>("SaidaAprovadosAnoAtual")
                        .HasColumnType("int");

                    b.Property<int>("SaidaRemanejamento")
                        .HasColumnType("int");

                    b.Property<int>("SerieAno")
                        .HasColumnType("int");

                    b.Property<bool>("UltimaSerieAno")
                        .HasColumnType("bit");

                    b.HasKey("IdPlanejamentoSerieAno");

                    b.HasIndex("IdPlanejamentoAnoLetivo");

                    b.ToTable("PlanejamentosSeriesAnos");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoTurma", b =>
                {
                    b.Property<int>("IdPlanejamentoTurma")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlanejamentoTurma"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<decimal>("CapacidadeFisicaAcordada")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("IdPlanejamentoSerieAno")
                        .HasColumnType("int");

                    b.Property<int>("IdSala")
                        .HasColumnType("int");

                    b.Property<string>("SiglaTurma")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("TipoAtendimento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TurnoPeriodo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPlanejamentoTurma");

                    b.HasIndex("IdPlanejamentoSerieAno");

                    b.HasIndex("IdSala");

                    b.ToTable("PlanejamentosTurmas");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Sala", b =>
                {
                    b.Property<int>("IdSala")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSala"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bloco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CapacidadeFisicaPadrao")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("IdEscola")
                        .HasColumnType("int");

                    b.Property<decimal>("Metragem")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSala");

                    b.HasIndex("IdEscola");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("ConfirmaSenha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Perfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoAnoLetivo", b =>
                {
                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.Escola", "Escola")
                        .WithMany("PlanejamentosAnosLetivos")
                        .HasForeignKey("IdEscola")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoMatriculaSequencial", b =>
                {
                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.Escola", "EscolaDestino")
                        .WithMany("PlanejamentosMatriculasSequenciaisDestinos")
                        .HasForeignKey("IdEscolaDestino")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.Escola", "EscolaOrigem")
                        .WithMany("PlanejamentosMatriculasSequenciaisOrigens")
                        .HasForeignKey("IdEscolaOrigem")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EscolaDestino");

                    b.Navigation("EscolaOrigem");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoSerieAno", b =>
                {
                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoAnoLetivo", "PlanejamentoAnoLetivo")
                        .WithMany("SeriesAnos")
                        .HasForeignKey("IdPlanejamentoAnoLetivo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlanejamentoAnoLetivo");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoTurma", b =>
                {
                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoSerieAno", "PlanejamentoSerieAno")
                        .WithMany("Turmas")
                        .HasForeignKey("IdPlanejamentoSerieAno")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.Sala", "Sala")
                        .WithMany("Turmas")
                        .HasForeignKey("IdSala")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlanejamentoSerieAno");

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Sala", b =>
                {
                    b.HasOne("PreMatriculasParanoa.Domain.Models.Entities.Escola", "Escola")
                        .WithMany("Salas")
                        .HasForeignKey("IdEscola")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Escola", b =>
                {
                    b.Navigation("PlanejamentosAnosLetivos");

                    b.Navigation("PlanejamentosMatriculasSequenciaisDestinos");

                    b.Navigation("PlanejamentosMatriculasSequenciaisOrigens");

                    b.Navigation("Salas");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoAnoLetivo", b =>
                {
                    b.Navigation("SeriesAnos");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.PlanejamentoSerieAno", b =>
                {
                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("PreMatriculasParanoa.Domain.Models.Entities.Sala", b =>
                {
                    b.Navigation("Turmas");
                });
#pragma warning restore 612, 618
        }
    }
}
