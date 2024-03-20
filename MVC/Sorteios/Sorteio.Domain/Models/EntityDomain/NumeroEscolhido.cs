using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class NumeroEscolhido
    {
        public int id_numero_escolhido { get; set; }
        public int id_pedido { get; set; }
        public int numero { get; set; }
    }
}
