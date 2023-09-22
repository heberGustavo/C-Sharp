using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class ObraDTO
    {
        [DataMember]
        public int ObrId { get; set; }

        [DataMember]
        public string ObrApelido { get; set; }

        [DataMember]
        public DateTime ObrDataInicio { get; set; }

        [DataMember]
        public DateTime ObrDataFim { get; set; }

        [DataMember]
        public string ObrEndereco { get; set; }

        [DataMember]
        public decimal ObrOrcamento { get; set; }

        [DataMember]
        public bool ObrStatus { get; set; }
    }
}
