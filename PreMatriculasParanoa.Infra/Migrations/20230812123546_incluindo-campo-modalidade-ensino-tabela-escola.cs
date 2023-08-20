using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class incluindocampomodalidadeensinotabelaescola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModalidadeEnsino",
                table: "Escolas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModalidadeEnsino",
                table: "Escolas");
        }
    }
}
