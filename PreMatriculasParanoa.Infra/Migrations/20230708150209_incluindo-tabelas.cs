using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class incluindotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    IdEscola = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.IdEscola);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentosMatriculasSequenciais",
                columns: table => new
                {
                    IdPlanejamentoMatriculaSequencial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnoLetivo = table.Column<int>(type: "int", nullable: false),
                    SerieAnoOrigem = table.Column<int>(type: "int", nullable: false),
                    SerieAnoDestino = table.Column<int>(type: "int", nullable: false),
                    IdEscolaOrigem = table.Column<int>(type: "int", nullable: false),
                    IdEscolaDestino = table.Column<int>(type: "int", nullable: false),
                    TotalMatriculas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentosMatriculasSequenciais", x => x.IdPlanejamentoMatriculaSequencial);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentosAnosLetivos",
                columns: table => new
                {
                    IdPlanejamentoAnoLetivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnoLetivo = table.Column<int>(type: "int", nullable: false),
                    DataInicioPlanejamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataTerminoPlanejamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEscola = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentosAnosLetivos", x => x.IdPlanejamentoAnoLetivo);
                    table.ForeignKey(
                        name: "FK_PlanejamentosAnosLetivos_Escolas_IdEscola",
                        column: x => x.IdEscola,
                        principalTable: "Escolas",
                        principalColumn: "IdEscola",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    IdSala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bloco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metragem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CapacidadeFisicaPadrao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdEscola = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.IdSala);
                    table.ForeignKey(
                        name: "FK_Salas_Escolas_IdEscola",
                        column: x => x.IdEscola,
                        principalTable: "Escolas",
                        principalColumn: "IdEscola",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentosSeriesAnos",
                columns: table => new
                {
                    IdPlanejamentoSerieAno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerieAno = table.Column<int>(type: "int", nullable: false),
                    PrimeiraSerieAno = table.Column<bool>(type: "bit", nullable: false),
                    UltimaSerieAno = table.Column<bool>(type: "bit", nullable: false),
                    EntradaAprovadosSerieAnoAnterior = table.Column<int>(type: "int", nullable: false),
                    EntradaRetidosSerieAnoAtual = table.Column<int>(type: "int", nullable: false),
                    EntradaSequencial = table.Column<int>(type: "int", nullable: false),
                    EntradaCentralMatricula = table.Column<int>(type: "int", nullable: false),
                    EntradaRemanejamento = table.Column<int>(type: "int", nullable: false),
                    SaidaRemanejamento = table.Column<int>(type: "int", nullable: false),
                    SaidaAprovadosUltimaSerieAno = table.Column<int>(type: "int", nullable: false),
                    IdPlanejamentoAnoLetivo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentosSeriesAnos", x => x.IdPlanejamentoSerieAno);
                    table.ForeignKey(
                        name: "FK_PlanejamentosSeriesAnos_PlanejamentosAnosLetivos_IdPlanejamentoAnoLetivo",
                        column: x => x.IdPlanejamentoAnoLetivo,
                        principalTable: "PlanejamentosAnosLetivos",
                        principalColumn: "IdPlanejamentoAnoLetivo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentosTurmas",
                columns: table => new
                {
                    IdPlanejamentoTurma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiglaTurma = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TurnoPeriodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoAtendimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacidadeFisicaAcordada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdSala = table.Column<int>(type: "int", nullable: false),
                    IdPlanejamentoSerieAno = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentosTurmas", x => x.IdPlanejamentoTurma);
                    table.ForeignKey(
                        name: "FK_PlanejamentosTurmas_PlanejamentosSeriesAnos_IdPlanejamentoSerieAno",
                        column: x => x.IdPlanejamentoSerieAno,
                        principalTable: "PlanejamentosSeriesAnos",
                        principalColumn: "IdPlanejamentoSerieAno",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanejamentosTurmas_Salas_IdSala",
                        column: x => x.IdSala,
                        principalTable: "Salas",
                        principalColumn: "IdSala",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosAnosLetivos_IdEscola",
                table: "PlanejamentosAnosLetivos",
                column: "IdEscola");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosSeriesAnos_IdPlanejamentoAnoLetivo",
                table: "PlanejamentosSeriesAnos",
                column: "IdPlanejamentoAnoLetivo");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosTurmas_IdPlanejamentoSerieAno",
                table: "PlanejamentosTurmas",
                column: "IdPlanejamentoSerieAno");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosTurmas_IdSala",
                table: "PlanejamentosTurmas",
                column: "IdSala");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_IdEscola",
                table: "Salas",
                column: "IdEscola");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanejamentosMatriculasSequenciais");

            migrationBuilder.DropTable(
                name: "PlanejamentosTurmas");

            migrationBuilder.DropTable(
                name: "PlanejamentosSeriesAnos");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "PlanejamentosAnosLetivos");

            migrationBuilder.DropTable(
                name: "Escolas");
        }
    }
}
