using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class FuncionarioDTO
    {
        [DataMember]
        public int FunId { get; set; }

        [DataMember]
        public string FunNome { get; set; }

        [DataMember]
        public decimal FunDiario { get; set; }

        [DataMember]
        public decimal FunMensal { get; set; }

        [DataMember]
        public DateTime FunDataContratacao { get; set; }

        [DataMember]
        public bool FunStatus { get; set; }
        
        [DataMember]
        public int FnfId { get; set; }

        [DataMember]
        public string FnfNome { get; set; }
    }
}
