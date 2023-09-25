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
        [StringLength(40, ErrorMessage = "O campo não deve ser maior que 40 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string FnfNome { get; set; }

    }
}
