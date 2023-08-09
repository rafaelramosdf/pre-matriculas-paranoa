using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreMatriculasParanoa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class incluindoregiaoescola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Regiao",
                table: "Escolas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "Escolas");
        }
    }
}
