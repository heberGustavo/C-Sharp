using Dapper.Contrib.Extensions;
using System;

namespace Sorteio.Data.EntityData
{
    [Table("VencedorSorteio")]
    public class VencedorSorteioData
    {
        [Key]
        public int id_vencedor_sorteio { get; set; }
        public int id_sorteio { get; set; }
        public int id_usuario { get; set; }
        public int numero_sorteado { get; set; }
        public DateTime data_sorteio { get; set; }
    }
}
