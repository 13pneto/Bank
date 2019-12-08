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

        [Display(Name = "Conta do cliente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int IdContaCliente { get; set; }

        [Display(Name = "Conta do cliente:")]
        //[Required(ErrorMessage = "Campo obrigatório!")]
        public Conta ContaDoCliente { get; set; }

        public Pessoa Pessoa{ get; set; }

        [Display(Name = "Limite:")]
        public double Limite { get; set; }

        public double Saldo { get; set; }


        public bool Status { get; set; }        //true = ativo | false = inativo
        public DateTime CriadoEm { get; set; }


        public ContaCliente()
        {
            this.Status = true;
            this.CriadoEm = DateTime.Now;
        }
    }
}
