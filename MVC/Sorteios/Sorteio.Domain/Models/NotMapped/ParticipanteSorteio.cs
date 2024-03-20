using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class ParticipanteSorteio
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public int id_status_pedido { get; set; }
        public int id_pedido { get; set; }
    }
}
