using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class DiasTrabalhadosDTO
    {
        [DataMember]
        public int Dit { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Funcionário é obrigatório")]
        public int FunId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Obra é obrigatório")]
        public int ObrId { get; set; }

    }
}
