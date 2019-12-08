using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ContasRetorno
    {
        public ContaCliente C1 { get; set; }
        public ContaCliente C2 { get; set; }

        public ContasRetorno(ContaCliente c1, ContaCliente c2)
        {
            C1 = c1;
            C2 = c2;
        }
        public ContasRetorno()
        {

        }
    }


}
