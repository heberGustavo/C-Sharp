using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestProjects.Data.EntityData
{
    [Table("Usuario")]
    public class UsuarioData
    {
        [Key]
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
    }
}
