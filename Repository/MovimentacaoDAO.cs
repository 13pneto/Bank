using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class MovimentacaoDAO : InterfaceDAO<Movimentacao>
    {
        private readonly Context _context;

        public MovimentacaoDAO(Context context)
        {
            _context = context;
        }

        public Movimentacao BuscarPorId(int id)
        {
            return _context.Movimentacoes.Include(x => x.ContaDestino).Include(x => x.ContaDestino.Pessoa).Include(x => x.ContaOrigem).Include(x => x.ContaOrigem.Pessoa).FirstOrDefault
            (x => x.IdMovimento.Equals(id));
        }

        public bool Cadastrar(Movimentacao m)
        {
            if (BuscarPorId(m.IdMovimento) == null)
            {
                _context.Movimentacoes.Add(m);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Movimentacao> ListarTodos()
        {
            return _context.Movimentacoes.Include(x => x.ContaDestino).Include(x => x.ContaDestino.Pessoa).Include(x => x.ContaOrigem).Include(x => x.ContaOrigem.Pessoa).ToList();
        }

        public List<Movimentacao> ListarTodos(int Id) //Listar todos as movimentacoes de determinado cliente
        {
            return _context.Movimentacoes.Include(x => x.ContaDestino).Include(x => x.ContaDestino.Pessoa).Include(x => x.ContaOrigem).Include(x => x.ContaOrigem.Pessoa).Where(x => x.ContaOrigem.IdContaCliente == Id).ToList();
        }

        public bool RemoverPorId(int id)
        {
            Movimentacao m = BuscarPorId(id);

            if (m != null)   //Verifica se existe este id no cadastro
            {
                _context.Movimentacoes.Remove(m);
                _context.SaveChanges();
                return true;
            }
            return false;                  //Retorna false caso nao encontre
        }
    }
}
