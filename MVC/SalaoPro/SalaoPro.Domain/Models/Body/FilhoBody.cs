using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.Models.Body
{
    public class FilhoBody
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data_de_nascimento { get; set; }
        public int id_funcionario { get; set; }
        public string nome_funcionario { get; set; }
    }
}
