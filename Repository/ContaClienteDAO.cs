using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ContaClienteDAO : InterfaceDAO<ContaCliente>
    {
        private readonly Context _context;
        private readonly MovimentacaoDAO _movimentacaoDAO;

        public ContaClienteDAO(Context context, MovimentacaoDAO movimentacaoDAO)
        {
            _context = context;
            _movimentacaoDAO = movimentacaoDAO;
        }


        public ContaCliente BuscarPorId(int id)
        {
            //return _context.ContaClientes.FirstOrDefault(x => x.IdContaCliente.Equals(id));
            return _context.ContaClientes.Include(x => x.Pessoa).Include(z => z.ContaDoCliente).FirstOrDefault(y => y.IdContaCliente.Equals(id));
        }

        public bool Cadastrar(ContaCliente c)
        {
            if (BuscarPorId(c.IdContaCliente) == null)
            {
                _context.ContaClientes.Add(c);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ContaCliente> ListarTodos()
        {
            return _context.ContaClientes.Include(x=> x.ContaDoCliente).ToList();
        }

        public bool RemoverPorId(int id)
        {
            ContaCliente c = BuscarPorId(id);

            if (c != null)   //Verifica se existe este id no cadastro
            {
                _context.ContaClientes.Remove(c);
                _context.SaveChanges();
                return true;
            }
            return false;                  //Retorna false caso nao encontre
        }

        //Metodo para adicionar saldo na conta do cliente
        public void AdicionarSaldo(ContaCliente c, double valor)
        {
            c.Saldo += valor;
            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RetirarSaldo(ContaCliente c, double valor)
        {
            c.Saldo -= valor;
            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool RealizaSaque(ContaCliente conta, double ValorSaque)
        {
            if (conta.Saldo >= ValorSaque)
            {
                Movimentacao m = new Movimentacao();

                conta.Saldo -= ValorSaque;

                m.DtMovimentacao = DateTime.Now;
                m.ContaOrigem = conta;              //
                m.ContaDestino = conta;
                m.TipoMovimentacao = "Saque";
                m.Valor = -ValorSaque;

                _movimentacaoDAO.Cadastrar(m);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public void RealizaDeposito(ContaCliente conta, double ValorDeposito)
        {
            Movimentacao m = new Movimentacao();

            conta.Saldo += ValorDeposito;

            m.DtMovimentacao = DateTime.Now;
            m.ContaOrigem = conta;              //
            m.ContaDestino = conta;
            m.Valor = ValorDeposito;
            m.TipoMovimentacao = "Deposito";

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
        }

        public bool RealizarTransferencia(ContaCliente contaOrigem, ContaCliente contaDestino, double ValorTransf)
        {
            Movimentacao m = new Movimentacao();
            double saldoContaOrigem = contaOrigem.Saldo;

            if (ValorTransf > saldoContaOrigem)
            {
                return false;
            }

            contaOrigem.Saldo -= ValorTransf;
            contaDestino.Saldo += ValorTransf;


            m.DtMovimentacao = DateTime.Now;
            m.ContaOrigem = contaOrigem;              //
            m.ContaDestino = contaDestino;              //
            m.TipoMovimentacao = "Transferencia";

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
            return true;
        }
    }
}
