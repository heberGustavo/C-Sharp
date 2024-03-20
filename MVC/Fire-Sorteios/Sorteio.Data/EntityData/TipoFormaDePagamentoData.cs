using Dapper.Contrib.Extensions;

namespace Sorteio.Data.EntityData
{
    [Table("TipoFormaDePagamento")]
    public class TipoFormaDePagamentoData
    {
        [Key]
        public int id_tipo_forma_de_pagamento { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }
    }
}
