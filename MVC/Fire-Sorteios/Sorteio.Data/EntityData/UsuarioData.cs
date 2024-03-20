using Dapper.Contrib.Extensions;
using System;

namespace Sorteio.Data.EntityData
{
    [Table("Usuario")]
    public class UsuarioData
    {
        [Key]
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string celular { get; set; }
        public bool status { get; set; }
        public int id_tipo_usuario { get; set; }
    }
}
