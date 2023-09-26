using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_FUNCIONARIO")]
    public class Funcionario
    {
        [Key]
        public int FunId { get; set; }
        public int FnfId { get; set; }
        public string FunNome { get; set; }
        public decimal? FunDiaria { get; set; }
        public decimal? FunMensal { get; set; }
        public DateTime FunDataContratacao { get; set; }
        public bool FunStatus { get; set; }
    }
}
