using Dapper.Contrib.Extensions;
using System;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("Obra")]
    public class ObraData
    {
        [Key]
        public int id_obra { get; set; }
        public string apelido { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public string endereco { get; set; }
        public decimal orcamento { get; set; }
        public bool excluido { get; set; }
    }
}
