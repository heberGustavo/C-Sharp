using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.Body
{
    public class CadastrarUsuarioListaNumerosBody
    {
        public decimal valor_total { get; set; }
        public int id_sorteio { get; set; }
        public Usuario usuario { get; set; }
        public IEnumerable<NumeroEscolhido> numeroSorteios { get; set; }
    }
}
