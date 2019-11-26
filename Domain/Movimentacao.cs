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

        [Display(Name = "Conta origem:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Conta ContaOrigem { get; set; }

        [Display(Name = "Conta destino:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Conta ContaDestino { get; set; }

        [Display(Name = "Valor:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
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
