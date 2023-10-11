using Dapper.Contrib.Extensions;
using System;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_MATERIAL")]
    public class Material
    {
        [Key]
        public int MtrId { get; set; }
        public int ObrId { get; set; }
        public string MtrNome { get; set; }
        public string MtrDescricao { get; set; }
        public decimal MtrValor { get; set; }
        public DateTime MtrDataCompra { get; set; }
    }
}
