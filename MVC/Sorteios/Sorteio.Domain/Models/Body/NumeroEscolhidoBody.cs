using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.Body
{
    public class NumeroEscolhidoBody
    {
        public int id_numero_escolhido { get; set; }
        public int id_pedido { get; set; }
        public int numero { get; set; }
        public int id_status_pedido { get; set; }
        public string nome_usuario { get; set; }
    }
}
