using Dapper.Contrib.Extensions;
using System;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_DIA_TRABALHADO")]
    public class DiaTrabalhado
    {
        [Key]
        public int DitId { get; set; }

        public int ObrId { get; set; }

        public DateTime DitData { get; set; }
    }
}
