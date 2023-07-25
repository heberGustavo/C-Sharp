using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.Models.EntityDomain
{
    public class Funcionario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data_de_nascimento { get; set; }
        public decimal salario { get; set; }
    }
}