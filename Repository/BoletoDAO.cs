using Domain;
using System;
using System.Collections.Generic;
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
        
        public bool Cadastrar(Boleto t)
        {
            throw new NotImplementedException();
        }

        public Boleto BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Boleto> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public bool RemoverPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
