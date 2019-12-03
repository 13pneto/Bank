using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AjusteClasseContaCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaIdCliente",
                table: "TB_ContaCliente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ContaCliente_PessoaIdCliente",
                table: "TB_ContaCliente",
                column: "PessoaIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ContaCliente_TB_Pessoa_PessoaIdCliente",
                table: "TB_ContaCliente",
                column: "PessoaIdCliente",
                principalTable: "TB_Pessoa",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ContaCliente_TB_Pessoa_PessoaIdCliente",
                table: "TB_ContaCliente");

            migrationBuilder.DropIndex(
                name: "IX_TB_ContaCliente_PessoaIdCliente",
                table: "TB_ContaCliente");

            migrationBuilder.DropColumn(
                name: "PessoaIdCliente",
                table: "TB_ContaCliente");
        }
    }
}
