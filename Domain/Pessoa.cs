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
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public char Tipo { get; set; }        //C - Cliente | F - Funcionario | A- Adm
        public List<Movimentacao> Movimentacoes{ get; set; }
    }
}
