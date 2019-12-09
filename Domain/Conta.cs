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
        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }
        [Display(Name = "Descrição:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; }
        public double Limite { get; set; }
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
