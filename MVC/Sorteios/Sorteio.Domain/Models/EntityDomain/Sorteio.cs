using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class Sorteio
    {
        public int id_sorteio { get; set; }
        public int id_categoria_sorteio { get; set; }
        public string nome { get; set; }
        public string edicao { get; set; }
        public decimal valor { get; set; }
        public int quantidade_numeros { get; set; }
        public string descricao_curta { get; set; }
        public string descricao_longa { get; set; }
        public bool status { get; set; }
        public bool excluido { get; set; }
    }
}
