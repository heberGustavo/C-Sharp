using Dapper.Contrib.Extensions;

namespace GerenciamentoDeProdutosAPI.Data.EntityData
{
    [Table("TB_CATEGORIA")]
    public class CategoriaData
    {
        [Key]
        public int CatId { get; set; }
        public string CatNome { get; set; }
        public bool CatSituacao { get; set; }
    }
}
