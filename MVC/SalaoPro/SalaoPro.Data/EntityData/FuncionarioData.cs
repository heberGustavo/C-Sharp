using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Data.EntityData
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
