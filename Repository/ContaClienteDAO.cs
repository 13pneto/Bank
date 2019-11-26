using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ContaClienteDAO : InterfaceDAO<ContaCliente>
    {
        private readonly Context _context;

        public ContaClienteDAO(Context context)
        {
            _context = context;
        }

        public ContaCliente BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Cadastrar(ContaCliente t)
        {
            throw new NotImplementedException();
        }

        public List<ContaCliente> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public bool RemoverPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
