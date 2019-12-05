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

        //[Display(Name = "Conta de Origem:")]
        //[Required(ErrorMessage = "Campo obrigatório!")]
        public ContaCliente ContaOrigem { get; set; }

        [Display(Name = "Data vencimento:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime DtVencimento { get; set; }

        [Display(Name = "Valor R$:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public double Valor { get; set; }

        public DateTime CriadoEm { get; set; }
        public string Status { get; set; }   
        
        /// <summary>
        /// STATUS: NP = NAO PAGO /// PG = PAGO /// ES = ESTORNADO
        /// </summary>
        public Boleto()
        {
            this.Status = "NP"; 
            this.CriadoEm = DateTime.Now;
        }

    }
}
