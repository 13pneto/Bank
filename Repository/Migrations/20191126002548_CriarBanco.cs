using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CriarBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Conta",
                columns: table => new
                {
                    IdConta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Conta", x => x.IdConta);
                });

            migrationBuilder.CreateTable(
                name: "TB_Pessoa",
                columns: table => new
                {
                    Nome = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Pessoa", x => x.Nome);
                });

            migrationBuilder.CreateTable(
                name: "TB_Boleto",
                columns: table => new
                {
                    IdBoleto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaOrigemIdConta = table.Column<int>(nullable: true),
                    DtVencimento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Boleto", x => x.IdBoleto);
                    table.ForeignKey(
                        name: "FK_TB_Boleto_TB_Conta_ContaOrigemIdConta",
                        column: x => x.ContaOrigemIdConta,
                        principalTable: "TB_Conta",
                        principalColumn: "IdConta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ContaCliente",
                columns: table => new
                {
                    IdContaCliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaDoClienteIdConta = table.Column<int>(nullable: true),
                    Limite = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Saldo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ContaCliente", x => x.IdContaCliente);
                    table.ForeignKey(
                        name: "FK_TB_ContaCliente_TB_Conta_ContaDoClienteIdConta",
                        column: x => x.ContaDoClienteIdConta,
                        principalTable: "TB_Conta",
                        principalColumn: "IdConta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Movimentacao",
                columns: table => new
                {
                    IdMovimento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaOrigemIdConta = table.Column<int>(nullable: true),
                    ContaDestinoIdConta = table.Column<int>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    DtMovimentacao = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    PessoaNome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Movimentacao", x => x.IdMovimento);
                    table.ForeignKey(
                        name: "FK_TB_Movimentacao_TB_Conta_ContaDestinoIdConta",
                        column: x => x.ContaDestinoIdConta,
                        principalTable: "TB_Conta",
                        principalColumn: "IdConta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Movimentacao_TB_Conta_ContaOrigemIdConta",
                        column: x => x.ContaOrigemIdConta,
                        principalTable: "TB_Conta",
                        principalColumn: "IdConta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Movimentacao_TB_Pessoa_PessoaNome",
                        column: x => x.PessoaNome,
                        principalTable: "TB_Pessoa",
                        principalColumn: "Nome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Boleto_ContaOrigemIdConta",
                table: "TB_Boleto",
                column: "ContaOrigemIdConta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ContaCliente_ContaDoClienteIdConta",
                table: "TB_ContaCliente",
                column: "ContaDoClienteIdConta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_ContaDestinoIdConta",
                table: "TB_Movimentacao",
                column: "ContaDestinoIdConta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_ContaOrigemIdConta",
                table: "TB_Movimentacao",
                column: "ContaOrigemIdConta");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Movimentacao_PessoaNome",
                table: "TB_Movimentacao",
                column: "PessoaNome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Boleto");

            migrationBuilder.DropTable(
                name: "TB_ContaCliente");

            migrationBuilder.DropTable(
                name: "TB_Movimentacao");

            migrationBuilder.DropTable(
                name: "TB_Conta");

            migrationBuilder.DropTable(
                name: "TB_Pessoa");
        }
    }
}
