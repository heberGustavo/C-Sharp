using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class Obra
    {
        public int id_obra { get; set; }
        public string apelido { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public string endereco { get; set; }
        public decimal orcamento { get; set; }
        public bool excluido { get; set; }
    }
}
