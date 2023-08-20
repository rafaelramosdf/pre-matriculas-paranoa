using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alterandotabelamatriculasequencialcampoperiodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerieAnoDestino",
                table: "PlanejamentosMatriculasSequenciais");

            migrationBuilder.RenameColumn(
                name: "SerieAnoOrigem",
                table: "PlanejamentosMatriculasSequenciais",
                newName: "PeriodoMatriculaSequencial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodoMatriculaSequencial",
                table: "PlanejamentosMatriculasSequenciais",
                newName: "SerieAnoOrigem");

            migrationBuilder.AddColumn<int>(
                name: "SerieAnoDestino",
                table: "PlanejamentosMatriculasSequenciais",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
