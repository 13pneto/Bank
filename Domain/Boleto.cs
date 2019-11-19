using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("TB_Boleto")]
    public class Boleto
    {
        [Key]
        public int IdBoleto { get; set; }
        public Conta ContaOrigem { get; set; }
        public DateTime DtVencimento { get; set; }
        public double Valor { get; set; }
        public DateTime CriadoEm { get; set; }
        public bool Status { get; set; }              //True = Ativo | False = Inativo (vencido/cancelado)

        public Boleto()
        {
            this.Status = true;
            this.CriadoEm = DateTime.Now;
        }

    }
}
