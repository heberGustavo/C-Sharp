using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class VencedorSorteio
    {
        public int id_vencedor_sorteio { get; set; }
        public int id_sorteio { get; set; }
        public int id_usuario { get; set; }
        public int numero_sorteado { get; set; }
        public DateTime data_sorteio { get; set; }
    }
}
