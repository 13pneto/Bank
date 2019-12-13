using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class sexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "TB_Pessoa",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "TB_Pessoa");
        }
    }
}
