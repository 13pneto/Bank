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
        private readonly ContaClienteDAO _contaClienteDAO;
        public BoletoDAO(Context context, MovimentacaoDAO movimentacaoDAO, ContaClienteDAO contaClienteDAO)
        {
            _context = context;
            _movimentacaoDAO = movimentacaoDAO;
            _contaClienteDAO = contaClienteDAO;
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
            _contaClienteDAO.AdicionarSaldo(b.ContaOrigem, b.Valor); //Adiciona saldo na conta

            //Gerar uma movimentação
            m.DtMovimentacao = DateTime.Now;
            m.ContaDestino = b.ContaOrigem;             //Para pagamento de boleto, o cliente origem e destino é a própria conta a receber, pois quem paga o boleto
            m.ContaOrigem = b.ContaOrigem;              //Não necessariamente deverá possuir uma conta, considera-se que o fiscal do sistema coletou o dinheiro e efetivou (loterica)
            m.TipoMovimentacao = "Boleto";
            m.Valor = b.Valor;

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
        }

        public bool EfetivarBoletoAPI(Boleto b)
        {
            Movimentacao m = new Movimentacao();

            if (b.Status == "PG" || b.Status == "ES")
            {
                return false;
            }

            b.Status = "PG";
            _context.Entry(b).State = EntityState.Modified;
            _contaClienteDAO.AdicionarSaldo(b.ContaOrigem, b.Valor); //Adiciona saldo na conta

            //Gerar uma movimentação
            m.DtMovimentacao = DateTime.Now;
            m.ContaDestino = b.ContaOrigem;             //Para pagamento de boleto, o cliente origem e destino é a própria conta a receber, pois quem paga o boleto
            m.ContaOrigem = b.ContaOrigem;              //Não necessariamente deverá possuir uma conta, considera-se que o fiscal do sistema coletou o dinheiro e efetivou (loterica)
            m.TipoMovimentacao = "Boleto";
            m.Valor = b.Valor;

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
            return true;
        }

        public void EstornarBoleto(Boleto b){

            Movimentacao m = new Movimentacao();
 

            b.Status = "ES";    //ES == ESTORNADO
            _context.Entry(b).State = EntityState.Modified;
            _contaClienteDAO.RetirarSaldo(b.ContaOrigem, b.Valor); //Remove saldo da conta

            //Gerar uma movimentação
            m.DtMovimentacao = DateTime.Now;
            m.ContaDestino = b.ContaOrigem;             //Para estorno de boleto, o valor será retirado da conta que gerou o boleto. Como quem paga é um usuário (lotérica) não tem como
            m.ContaOrigem = b.ContaOrigem;              //o sistema controlar a devolução, apenas será informado o estorno e caixa deverá voltar o valor ao cliente pagador
            m.TipoMovimentacao = "Estorno Boleto";
            m.Valor = b.Valor;

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();

}


        public bool EstornarBoletoAPI(Boleto b)
        {

            Movimentacao m = new Movimentacao();

            if(b.Status == "PG") { 

            b.Status = "ES";    //ES == ESTORNADO
            _context.Entry(b).State = EntityState.Modified;
            _contaClienteDAO.RetirarSaldo(b.ContaOrigem, b.Valor); //Remove saldo da conta

            //Gerar uma movimentação
            m.DtMovimentacao = DateTime.Now;
            m.ContaDestino = b.ContaOrigem;             //Para estorno de boleto, o valor será retirado da conta que gerou o boleto. Como quem paga é um usuário (lotérica) não tem como
            m.ContaOrigem = b.ContaOrigem;              //o sistema controlar a devolução, apenas será informado o estorno e caixa deverá voltar o valor ao cliente pagador
            m.TipoMovimentacao = "Estorno Boleto";
            m.Valor = b.Valor;

            _movimentacaoDAO.Cadastrar(m);
            _context.SaveChanges();
            return true;
            }

            return false;

        }

    }
}
