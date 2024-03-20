using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class TipoFormaDePagamento
    {
        public int id_tipo_forma_de_pagamento { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
