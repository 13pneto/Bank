using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class BoletoDAO : InterfaceDAO<Boleto>
    {
        private readonly Context _context;
        private readonly MovimentacaoDAO _movimentacaoDAO;
        public BoletoDAO(Context context, MovimentacaoDAO movimentacaoDAO)
        {
            _context = context;
            _movimentacaoDAO = movimentacaoDAO;
        }

        public bool Cadastrar(Boleto b)
        {
            if (BuscarPorId(b.IdBoleto) == null)    //Verifica se é nulo (nao cadastrado ainda)
            {
                _context.Boletos.Add(b);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boleto BuscarPorId(int id)
        {
            return _context.Boletos.Include(x => x.ContaOrigem).Include(x => x.ContaOrigem.Pessoa).Include(v => v.ContaOrigem.ContaDoCliente).FirstOrDefault
            (x => x.IdBoleto.Equals(id));
        }

        public List<Boleto> ListarTodos()
        {
            return _context.Boletos.Include(x => x.ContaOrigem).Include(v =>v.ContaOrigem.Pessoa).Include(z => z.ContaOrigem.ContaDoCliente).ToList();
        }

        public bool RemoverPorId(int id)
        {
            Boleto b = BuscarPorId(id);

            if (b != null)   //Verifica se existe este id no cadastro
            {
                _context.Boletos.Remove(b);
                _context.SaveChanges();
                return true;
            }
            return false;                  //Retorna false caso nao encontre
        }

        //Efetivar o pagamento de um boleto
        public void EfetivarBoleto(Boleto b)
        {
            Movimentacao m = new Movimentacao();

            b.Status = "PG";
            _context.Entry(b).State = EntityState.Modified;
            ContaClienteDAO.AdicionarSaldo(b.ContaOrigem, b.Valor); //Adiciona saldo na conta

            //Gerar uma movimentação
            m.DtMovimentacao = DateTime.Now;
            m.ContaDestino = b.ContaOrigem;             //Para pagamento de boleto, o cliente origem e destino é a própria conta a receber, pois quem paga o boleto
            m.ContaOrigem = b.ContaOrigem;              //Não necessariamente deverá possuir uma conta, considera-se que o fiscal do sistema coletou o dinheiro e efetivou (loterica)
            m.TipoMovimentacao = "Boleto";

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
        }
    }
}
