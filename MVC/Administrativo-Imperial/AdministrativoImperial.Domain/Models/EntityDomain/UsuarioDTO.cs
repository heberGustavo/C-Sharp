﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class UsuarioDTO
    {
        [DataMember]
        public int UsaId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string UsaNome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string UsaEmail { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string senha { get; set; }
        public byte[] UsaSenha { get; set; }

        [DataMember]
        public byte[] UsaSalt { get; set; }

    }
}
