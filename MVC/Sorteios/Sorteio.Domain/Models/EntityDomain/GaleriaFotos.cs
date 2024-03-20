using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class GaleriaFotos
    {
        public int id_galeria_fotos { get; set; }
        public string url_imagem { get; set; }
        public bool status { get; set; }
        public int id_sorteio { get; set; }
    }
}
