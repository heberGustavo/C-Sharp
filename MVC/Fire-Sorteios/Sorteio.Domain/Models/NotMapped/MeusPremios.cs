using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class MeusPremios
    {
        public int id_pedido { get; set; }
        public string nome_sorteio { get; set; }
        public DateTime data_sorteio { get; set; }
        public int numero_sorteado { get; set; }
    }
}
