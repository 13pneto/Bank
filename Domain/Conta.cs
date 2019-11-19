using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("TB_Conta")]
    public class Conta
    {
        [Key]
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        //public double Limite { get; set; }
        public bool Status { get; set; }        //true = ativo | false = inativo
        public DateTime CriadoEm { get; set; }
        //public double Saldo { get; set; }

        public Conta()
        {
            this.Status = true;
            this.CriadoEm = DateTime.Now;
        }

    }
}
