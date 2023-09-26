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
        [StringLength(40, ErrorMessage = "O campo não deve ser maior que 40 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string FunNome { get; set; }

        [DataMember]
        public decimal FunDiaria { get; set; }

        [DataMember]
        public decimal FunMensal { get; set; }

        [DataMember]
        public DateTime FunDataContratacao { get; set; }

        [DataMember]
        public bool FunStatus { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Função do Funcionário é obrigatório")]
        public int FnfId { get; set; }

        [DataMember]
        public string NomeFuncao { get; set; }
    }
}
