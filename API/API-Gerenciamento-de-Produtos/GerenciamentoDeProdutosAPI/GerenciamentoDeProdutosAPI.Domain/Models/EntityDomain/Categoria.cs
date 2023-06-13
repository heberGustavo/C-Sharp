using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Situacao { get; set; }
    }
}
