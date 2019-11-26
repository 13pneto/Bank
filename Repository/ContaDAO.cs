using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ContaDAO : InterfaceDAO<Conta>
    {
        private readonly Context _context;

        public ContaDAO(Context context)
        {
            _context = context;
        }

        public Conta BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Cadastrar(Conta t)
        {
            throw new NotImplementedException();
        }

        public List<Conta> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public bool RemoverPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
