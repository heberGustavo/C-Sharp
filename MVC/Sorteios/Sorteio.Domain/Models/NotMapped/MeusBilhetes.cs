using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class MeusBilhetes
    {
        public int id_pedido { get; set; }
        public string nome_sorteio { get; set; }
        public DateTime data_compra_sorteio { get; set; }
        public int numero { get; set; }
        public int id_status_pedido { get; set; }
        public decimal valor { get; set; }
    }
}
