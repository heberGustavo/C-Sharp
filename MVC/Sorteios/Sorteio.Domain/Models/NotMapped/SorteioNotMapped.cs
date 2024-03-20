using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class SorteioNotMapped
    {
        public int id_sorteio { get; set; }
        public int id_categoria_sorteio { get; set; }
        public string nome_sorteio { get; set; }
        public string edicao_sorteio { get; set; }
        public decimal valor { get; set; }
        public int quantidade_numeros { get; set; }
        public string descricao_curta { get; set; }
        public string descricao_longa { get; set; }
        public bool status { get; set; }
        public bool excluido { get; set; }

        public int id_usuario { get; set; }
        public int id_vencedor_sorteio { get; set; }
        public string nome_ganhador { get; set; }
        public int numero_sorteado { get; set; }
        public DateTime data_sorteio { get; set; }

        public int quantidade_imagens { get; set; }
    }
}
