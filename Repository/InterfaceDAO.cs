using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    interface InterfaceDAO<T>
    {

        bool Cadastrar(T t); //true = cadastrado;

        List<T> ListarTodos();

        T BuscarPorId(int id);

        bool RemoverPorId(int id);

    }
}
