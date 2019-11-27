using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Boleto_TB_Conta_ContaOrigemIdConta",
                table: "TB_Boleto");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaDestinoIdConta",
                table: "TB_Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaOrigemIdConta",
                table: "TB_Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Pessoa_PessoaNome",
                table: "TB_Movimentacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Pessoa",
                table: "TB_Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_TB_Movimentacao_PessoaNome",
                table: "TB_Movimentacao");

            migrationBuilder.DropColumn(
                name: "PessoaNome",
                table: "TB_Movimentacao");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "TB_Pessoa",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TB_Pessoa",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "TB_Pessoa",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdConta",
                table: "TB_Movimentacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoIdConta",
                table: "TB_Movimentacao",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PessoaIdCliente",
                table: "TB_Movimentacao",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TB_Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "TB_Conta",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdConta",
                table: "TB_Boleto",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Pessoa",
                table: "TB_Pessoa",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_PessoaIdCliente",
                table: "TB_Movimentacao",
                column: "PessoaIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Boleto_TB_Conta_ContaOrigemIdConta",
                table: "TB_Boleto",
                column: "ContaOrigemIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                column: "ContaDoClienteIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaDestinoIdConta",
                table: "TB_Movimentacao",
                column: "ContaDestinoIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaOrigemIdConta",
                table: "TB_Movimentacao",
                column: "ContaOrigemIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Pessoa_PessoaIdCliente",
                table: "TB_Movimentacao",
                column: "PessoaIdCliente",
                principalTable: "TB_Pessoa",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Boleto_TB_Conta_ContaOrigemIdConta",
                table: "TB_Boleto");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaDestinoIdConta",
                table: "TB_Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaOrigemIdConta",
                table: "TB_Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Movimentacao_TB_Pessoa_PessoaIdCliente",
                table: "TB_Movimentacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Pessoa",
                table: "TB_Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_TB_Movimentacao_PessoaIdCliente",
                table: "TB_Movimentacao");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "TB_Pessoa");

            migrationBuilder.DropColumn(
                name: "PessoaIdCliente",
                table: "TB_Movimentacao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TB_Pessoa",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "TB_Pessoa",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdConta",
                table: "TB_Movimentacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoIdConta",
                table: "TB_Movimentacao",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "PessoaNome",
                table: "TB_Movimentacao",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TB_Conta",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "TB_Conta",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemIdConta",
                table: "TB_Boleto",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Pessoa",
                table: "TB_Pessoa",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_PessoaNome",
                table: "TB_Movimentacao",
                column: "PessoaNome");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Boleto_TB_Conta_ContaOrigemIdConta",
                table: "TB_Boleto",
                column: "ContaOrigemIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                column: "ContaDoClienteIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaDestinoIdConta",
                table: "TB_Movimentacao",
                column: "ContaDestinoIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Conta_ContaOrigemIdConta",
                table: "TB_Movimentacao",
                column: "ContaOrigemIdConta",
                principalTable: "TB_Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Movimentacao_TB_Pessoa_PessoaNome",
                table: "TB_Movimentacao",
                column: "PessoaNome",
                principalTable: "TB_Pessoa",
                principalColumn: "Nome",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
