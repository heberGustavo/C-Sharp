using System;
using System.Collections.Generic;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class DiaTrabalhadoDTO
    {
        public int DitId { get; set; }

        public int ObrId { get; set; }

        public string ObrApelido { get; set; }

        public DateTime DitData { get; set; }

        
        public string[] FunIds { get; set; }
        public List<DiaTrabalhadoFuncionarioDTO> DiaTrabalhadoFuncionarios { get; set; }

    }
}
