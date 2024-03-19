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
        [StringLength(40, ErrorMessage = "O campo não deve ser maior que 40 caracteres")]
        [Required(ErrorMessage = "O campo Apelido é obrigatório")]
        public string ObrApelido { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Data Início é obrigatório")]
        public DateTime ObrDataInicio { get; set; }

        [DataMember]
        public DateTime ObrDataFim { get; set; }

        [DataMember]
        [StringLength(100, ErrorMessage = "O campo não deve ser maior que 100 caracteres")]
        [Required(ErrorMessage = "O campo Endereço é obrigatório")]
        public string ObrEndereco { get; set; }

        [DataMember]
        public decimal ObrOrcamento { get; set; }

        [DataMember]
        public bool ObrStatus { get; set; }

        public decimal GastosTotais { get; set; }
    }
}
