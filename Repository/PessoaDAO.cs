using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class PessoaDAO : InterfaceDAO<Pessoa>
    {
        private readonly Context _context;
        private readonly PessoaDAO _pessoaDAO;

        public PessoaDAO(Context context, PessoaDAO pessoaDAO)
        {
            _context = context;
            _pessoaDAO = pessoaDAO;
        }


        public Pessoa BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Cadastrar(Pessoa t)
        {
            throw new NotImplementedException();
        }

        public List<Pessoa> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public bool RemoverPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
