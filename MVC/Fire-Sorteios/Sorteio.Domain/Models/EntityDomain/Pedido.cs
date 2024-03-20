using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class Pedido
    {
        public int id_pedido { get; set; }
        public int id_usuario { get; set; }
        public int id_sorteio { get; set; }
        public DateTime data_pedido { get; set; }
        public decimal valor_total { get; set; }
        public int id_status_pedido { get; set; }
    }
}