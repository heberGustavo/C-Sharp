using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class FuncaoFuncionarioDTO
    {
        [DataMember]
        public int FnfId { get; set; }

        [DataMember]
        public string FnfNome { get; set; }

        [DataMember]
        public bool FnfStatus { get; set; }
    }
}
