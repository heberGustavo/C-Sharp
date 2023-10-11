using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdministrativoImperial.Domain.Models.EntityDomain
{
    public class MaterialDTO
    {
        [DataMember]
        public int MtrId { get; set; }

        [DataMember]
        public int ObrId { get; set; }
        public string ObrApelido { get; set; }

        [DataMember]
        [StringLength(40, ErrorMessage = "O campo não deve ser maior que 40 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string MtrNome { get; set; }

        [DataMember]
        [StringLength(200, ErrorMessage = "O campo não deve ser maior que 200 caracteres")]
        public string MtrDescricao { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        public decimal MtrValor { get; set; }

        [DataMember]
        [Required(ErrorMessage = "O campo Data é obrigatório")]
        public DateTime MtrDataCompra { get; set; }

    }
}
