using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("TB_ContaCliente")]
    public class ContaCliente
    {
        [Key]
        public int IdContaCliente { get; set; }
        public Conta ContaDoCliente { get; set; }
        public double Limite { get; set; }
        public bool Status { get; set; }        //true = ativo | false = inativo
        public DateTime CriadoEm { get; set; }
        public double Saldo { get; set; }


        public ContaCliente()
        {
            this.Status = true;
            this.CriadoEm = DateTime.Now;
        }
    }
}
