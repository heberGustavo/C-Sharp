using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.Body
{
    public class FormaDePagamentoBody
    {
        public int id_forma_de_pagamento { get; set; }
        public string nome_banco { get; set; }
        public string codigo_banco { get; set; }
        public string favorecido { get; set; }
        public string cpf { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
        public string url_imagem { get; set; }
        public int id_tipo_forma_de_pagamento { get; set; }
        public string nome_tipo_forma_de_pagamento { get; set; }
        public bool status { get; set; }
        public string pix { get; set; }
    }
}
