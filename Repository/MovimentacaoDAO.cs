using Domain;
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
            return _context.Movimentacoes.FirstOrDefault
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
            return _context.Movimentacoes.ToList();
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
