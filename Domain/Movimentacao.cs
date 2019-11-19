using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("TB_Movimentacao")]
    public class Movimentacao
    {
        [Key]
        public int IdMovimento { get; set; }
        public Conta ContaOrigem { get; set; }
        public Conta ContaDestino { get; set; }
        public double Valor { get; set; }
        public DateTime DtMovimentacao { get; set; }
        public bool Status { get; set; }            //True = ativo | False = inativo

        public Movimentacao()
        {
            this.Status = true;
            this.DtMovimentacao = DateTime.Now;
        }
    }
}
