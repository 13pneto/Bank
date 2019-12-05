using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class UpdateMovimentacaoContaCliente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Boleto_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Boleto");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_TB_Movimentacao_ContaOrigemIdContaCliente",
                table: "TB_Movimentacao");

            migrationBuilder.DropColumn(
                name: "ContaOrigemIdContaCliente",
                table: "TB_Movimentacao");

            migrationBuilder.AddColumn<int>(
                name: "FK_ContaOrigem",
                table: "TB_Movimentacao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdContaCliente",
                table: "TB_Boleto",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_FK_ContaOrigem",
                table: "TB_Movimentacao",
                column: "FK_ContaOrigem");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Boleto_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Boleto",
                column: "ContaOrigemIdContaCliente",
                principalTable: "TB_ContaCliente",
                principalColumn: "IdContaCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_ContaCliente_FK_ContaOrigem",
                table: "TB_Movimentacao",
                column: "FK_ContaOrigem",
                principalTable: "TB_ContaCliente",
                principalColumn: "IdContaCliente",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Boleto_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Boleto");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_ContaCliente_FK_ContaOrigem",
                table: "TB_Movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_TB_Movimentacao_FK_ContaOrigem",
                table: "TB_Movimentacao");

            migrationBuilder.DropColumn(
                name: "FK_ContaOrigem",
                table: "TB_Movimentacao");

            migrationBuilder.AddColumn<int>(
                name: "ContaOrigemIdContaCliente",
                table: "TB_Movimentacao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdContaCliente",
                table: "TB_Boleto",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_ContaOrigemIdContaCliente",
                table: "TB_Movimentacao",
                column: "ContaOrigemIdContaCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Boleto_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Boleto",
                column: "ContaOrigemIdContaCliente",
                principalTable: "TB_ContaCliente",
                principalColumn: "IdContaCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_ContaCliente_ContaOrigemIdContaCliente",
                table: "TB_Movimentacao",
                column: "ContaOrigemIdContaCliente",
                principalTable: "TB_ContaCliente",
                principalColumn: "IdContaCliente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
