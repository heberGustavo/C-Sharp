using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AdministrativoImperial.Domain.Models.Body
{
    public class UsuarioBody
    {
        [DataMember]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string email { get; set; }

        [DataMember]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string senha { get; set; }
    }
}
