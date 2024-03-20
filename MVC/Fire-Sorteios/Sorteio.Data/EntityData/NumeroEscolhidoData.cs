using Dapper.Contrib.Extensions;

namespace Sorteio.Data.EntityData
{
    [Table("NumeroEscolhido")]
    public class NumeroEscolhidoData
    {
        [Key]
        public int id_numero_escolhido { get; set; }
        public int id_pedido { get; set; }
        public int numero { get; set; }
    }
}
