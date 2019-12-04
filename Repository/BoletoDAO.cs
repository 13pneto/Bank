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

        public BoletoDAO(Context context)
        {
            _context = context;
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
            return _context.Boletos.FirstOrDefault
            (x => x.IdBoleto.Equals(id));
        }

        public List<Boleto> ListarTodos()
        {
            return _context.Boletos.Include(x => x.ContaOrigem).Include(y => y.ContaOrigem.Pessoa).Include(z => z.ContaOrigem.ContaDoCliente).ToList();
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
            b.Status = "PG";
            _context.Entry(b).State = EntityState.Modified;
            ContaClienteDAO.AdicionarSaldo(b.ContaOrigem, b.Valor);
            _context.SaveChanges();
        }
    }
}
