using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Situacao { get; set; }
        public int Cat_Id { get; set; }
    }
}
