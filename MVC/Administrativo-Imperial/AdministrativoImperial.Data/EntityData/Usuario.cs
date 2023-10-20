using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int UsaId { get; set; }
        public string UsaNome { get; set; }
        public string UsaEmail { get; set; }
        public string UsaSenha { get; set; }
        public string UsaSalt { get; set; }
    }
}