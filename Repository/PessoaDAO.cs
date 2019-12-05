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

        public PessoaDAO(Context context)
        {
            _context = context;
        }

        public Pessoa BuscarPorId(int id)
        {
            return _context.Pessoas.FirstOrDefault(x => x.IdCliente.Equals(id));
        }

        public bool BuscarPorCpf(string cpf, char tipo)
        {
            Pessoa p = _context.Pessoas.FirstOrDefault(x => x.Cpf.Equals(cpf) && x.Tipo.Equals(tipo));
            if (p == null)
            {
                return false;
            }
            return true;
        }

        public Pessoa BuscarPorCpf(string cpf)
        {
            return _context.Pessoas.FirstOrDefault(x => x.Cpf.Equals(cpf));
        }

        public bool Cadastrar(Pessoa p)
        {
            if (BuscarPorCpf(p.Cpf, p.Tipo) == true)
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
