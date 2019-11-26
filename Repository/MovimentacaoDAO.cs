using Domain;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public bool Cadastrar(Movimentacao t)
        {
            throw new NotImplementedException();
        }

        public List<Movimentacao> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public bool RemoverPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
