using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class UsuarioDTO
    {
        public int UsaId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string UsaNome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string UsaEmail { get; set; }

        public string senha { get; set; }
        public byte[] UsaSenha { get; set; }

        public byte[] UsaSalt { get; set; }

    }
}
