using Dapper.Contrib.Extensions;
using System;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_OBRA")]
    public class Obra
    {
        [Key]
        public int ObrId { get; set; }
        public string ObrApelido { get; set; }
        public DateTime ObrDataInicio { get; set; }
        public DateTime ObrDataFim { get; set; }
        public string ObrEndereco { get; set; }
        public decimal ObrOrcamento { get; set; }
        public bool ObrStatus { get; set; }
    }
}
