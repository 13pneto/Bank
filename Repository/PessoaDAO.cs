using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            return _context.Pessoas.FirstOrDefault
             (x => x.IdCliente.Equals(id));
        }

        public bool Cadastrar(Pessoa p)
        {
            if (BuscarPorId(p.IdCliente) == null)
            {
                _context.Pessoas.Add(p);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Pessoa> ListarTodos()
        {
            return _context.Pessoas.ToList();
        }

        public bool RemoverPorId(int id)
        {
            Pessoa p = BuscarPorId(id);

            if (p != null)   //Verifica se existe este id no cadastro
            {
                _context.Pessoas.Remove(p);
                _context.SaveChanges();
                return true;
            }
            return false;                  //Retorna false caso nao encontre
        }
    }
}
