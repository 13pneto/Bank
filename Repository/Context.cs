using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Boleto> Boletos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<ContaCliente> ContaClientes { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
