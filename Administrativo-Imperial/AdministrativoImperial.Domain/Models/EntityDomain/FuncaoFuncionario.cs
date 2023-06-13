using System;
using System.ComponentModel.DataAnnotations;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class FuncaoFuncionario
    {
        public int id_funcao_funcionario { get; set; }
        public string nome { get; set; }
        public bool excluido { get; set; }
    }
}
