using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Contas.FirstOrDefault
            (x => x.IdConta.Equals(id));
        }


        public bool Cadastrar(Conta c)
        {
            if (BuscarPorId(c.IdConta) == null)
            {
                _context.Contas.Add(c);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Conta> ListarTodos()
        {
            return _context.Contas.ToList();
        }

        public bool RemoverPorId(int id)
        {
            Conta c = BuscarPorId(id);

            if (c != null)   //Verifica se existe este id no cadastro
            {
                _context.Contas.Remove(c);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
