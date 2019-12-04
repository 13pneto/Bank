using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //return _context.ContaClientes.FirstOrDefault(x => x.IdContaCliente.Equals(id));
            return _context.ContaClientes.Include(x => x.Pessoa).Include(z => z.ContaDoCliente).FirstOrDefault(y => y.IdContaCliente.Equals(id));
        }

        public bool Cadastrar(ContaCliente c)
        {
            if (BuscarPorId(c.IdContaCliente) == null)
            {
                _context.ContaClientes.Add(c);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ContaCliente> ListarTodos()
        {
            return _context.ContaClientes.ToList();
        }

        public bool RemoverPorId(int id)
        {
            ContaCliente c = BuscarPorId(id);

            if (c != null)   //Verifica se existe este id no cadastro
            {
                _context.ContaClientes.Remove(c);
                _context.SaveChanges();
                return true;
            }
            return false;                  //Retorna false caso nao encontre
        }

        //Metodo para adicionar saldo na conta do cliente
        public static void AdicionarSaldo(ContaCliente c, double valor)
        {
            c.Saldo += valor;
        }
    }
}
