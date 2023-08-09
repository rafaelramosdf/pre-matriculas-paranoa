using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alterandocampotabelaplanejamentoserieano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaidaAprovadosUltimaSerieAno",
                table: "PlanejamentosSeriesAnos",
                newName: "SaidaAprovadosAnoAtual");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaidaAprovadosAnoAtual",
                table: "PlanejamentosSeriesAnos",
                newName: "SaidaAprovadosUltimaSerieAno");
        }
    }
}
