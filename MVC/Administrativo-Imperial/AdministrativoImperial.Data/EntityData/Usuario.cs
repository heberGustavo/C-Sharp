using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_USUARIO")]
    public class Usuario
    {
        [Key]
        public int UsaId { get; set; }
        public string UsaNome { get; set; }
        public string UsaEmail { get; set; }
        public byte[] UsaSenha { get; set; }
        public byte[] UsaSalt { get; set; }
    }
}