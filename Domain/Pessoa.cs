using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("TB_Pessoa")]
    public class Pessoa
    {
        [Key]
        public int IdCliente { get; set; }

        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "CPF:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(11, ErrorMessage = "Maximo 11 caracteres!")]
        public string Cpf { get; set; }

        [Display(Name = "Conta do cliente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public bool Status { get; set; }


        public DateTime CriadoEm { get; set; }
        public char Tipo { get; set; }        //C - Cliente | F - Funcionario | A- Adm
        //public List<Movimentacao> Movimentacoes{ get; set; }
    }
}
