using Dapper.Contrib.Extensions;

namespace Sorteio.Data.EntityData
{
    [Table("GaleriaFotos")]
    public class GaleriaFotosData
    {
        [Key]
        public int id_galeria_fotos { get; set; }
        public string url_imagem { get; set; }
        public bool status { get; set; }
        public int id_sorteio { get; set; }
    }
}
