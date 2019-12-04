using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class UpdateStatusBoleto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TB_Boleto",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                column: "ContaDoClienteIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "TB_Boleto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                column: "ContaDoClienteIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
