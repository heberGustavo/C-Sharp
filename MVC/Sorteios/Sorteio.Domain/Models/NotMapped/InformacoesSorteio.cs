using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.NotMapped
{
    public class InformacoesSorteio
    {
        public int id_sorteio { get; set; }
        public string nome { get; set; }
        public string edicao { get; set; }
        public decimal valor { get; set; }
        public int quantidade_numeros { get; set; }
        public bool status { get; set; }
        public bool excluido { get; set; }

        public string nome_ganhador { get; set; }
        public int numero_sorteado { get; set; }
        public DateTime data_sorteio { get; set; }

        public string url_imagem { get; set; }

        public int numeros_reservados { get; set; }
        public int numeros_pagos { get; set; }

    }
}
