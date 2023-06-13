using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.Data.EntityData
{
    [Table("TB_CATEGORIA")]
    public class CategoriaData
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Situacao { get; set; }
    }
}
