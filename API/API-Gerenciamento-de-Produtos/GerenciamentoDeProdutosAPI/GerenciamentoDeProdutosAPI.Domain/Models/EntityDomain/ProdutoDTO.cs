using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain
{
    public class ProdutoDTO
    {
        public int ProId { get; set; }
        public string ProNome { get; set; }
        public string ProDescricao { get; set; }
        public decimal ProPreco { get; set; }
        public bool ProSituacao { get; set; }
        public int CatId { get; set; }
    }
}
