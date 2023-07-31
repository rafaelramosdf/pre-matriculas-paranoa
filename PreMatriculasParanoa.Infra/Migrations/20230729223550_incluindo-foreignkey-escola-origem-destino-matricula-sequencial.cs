using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class incluindoforeignkeyescolaorigemdestinomatriculasequencial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosMatriculasSequenciais_IdEscolaDestino",
                table: "PlanejamentosMatriculasSequenciais",
                column: "IdEscolaDestino");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentosMatriculasSequenciais_IdEscolaOrigem",
                table: "PlanejamentosMatriculasSequenciais",
                column: "IdEscolaOrigem");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentosMatriculasSequenciais_Escolas_IdEscolaDestino",
                table: "PlanejamentosMatriculasSequenciais",
                column: "IdEscolaDestino",
                principalTable: "Escolas",
                principalColumn: "IdEscola",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentosMatriculasSequenciais_Escolas_IdEscolaOrigem",
                table: "PlanejamentosMatriculasSequenciais",
                column: "IdEscolaOrigem",
                principalTable: "Escolas",
                principalColumn: "IdEscola",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentosMatriculasSequenciais_Escolas_IdEscolaDestino",
                table: "PlanejamentosMatriculasSequenciais");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentosMatriculasSequenciais_Escolas_IdEscolaOrigem",
                table: "PlanejamentosMatriculasSequenciais");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentosMatriculasSequenciais_IdEscolaDestino",
                table: "PlanejamentosMatriculasSequenciais");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentosMatriculasSequenciais_IdEscolaOrigem",
                table: "PlanejamentosMatriculasSequenciais");
        }
    }
}
