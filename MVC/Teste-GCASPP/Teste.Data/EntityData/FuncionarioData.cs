using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Data.EntityData
{
    [Table("TB_FUNCIONARIO")]
    public class FuncionarioData
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data_de_nascimento { get; set; }
        public decimal salario { get; set; }
    }
}
