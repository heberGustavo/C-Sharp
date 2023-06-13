using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class Funcionario
    {
        public int id_funcionario { get; set; }
        public string nome { get; set; }
        public decimal diaria { get; set; }
        public decimal mensal { get; set; }
        public DateTime data_contratacao { get; set; }
        public int id_funcao_funcionario { get; set; }
        public string nome_funcao { get; set; }
        public bool excluido { get; set; }
    }
}
