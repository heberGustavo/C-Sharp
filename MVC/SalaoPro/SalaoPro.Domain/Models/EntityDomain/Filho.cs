using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.Models.EntityDomain
{
    public class Filho
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data_de_nascimento { get; set; }
        public int id_funcionario { get; set; }
    }
}
