using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class CategoriaSorteio
    {
        public int? id_categoria_sorteio { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
