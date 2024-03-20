using Dapper.Contrib.Extensions;

namespace Sorteio.Data.EntityData
{
    [Table("CategoriaSorteio")]
    public class CategoriaSorteioData
    {
        [Key]
        public int id_categoria_sorteio { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
