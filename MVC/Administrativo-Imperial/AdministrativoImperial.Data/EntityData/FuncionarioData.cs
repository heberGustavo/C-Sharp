using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("Funcionario")]
    public class FuncionarioData
    {
        [Key]
        public int id_funcionario { get; set; }
        public string nome { get; set; }
        public decimal? diaria { get; set; }
        public decimal? mensal { get; set; }
        public DateTime data_contratacao { get; set; }
        public int id_funcao_funcionario { get; set; }
        public bool excluido { get; set; }
    }
}
