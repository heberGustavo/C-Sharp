using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.EntityDomain
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string celular { get; set; }
        public bool status { get; set; }
        public int id_tipo_usuario { get; set; }
        public int id_usuario_cadastrado { get; set; }

    }
}
