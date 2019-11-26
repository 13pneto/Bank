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

        [Display(Name = "Conta de Origem:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Conta ContaOrigem { get; set; }

        [Display(Name = "Data vencimento:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime DtVencimento { get; set; }

        [Display(Name = "Valor R$:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
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
