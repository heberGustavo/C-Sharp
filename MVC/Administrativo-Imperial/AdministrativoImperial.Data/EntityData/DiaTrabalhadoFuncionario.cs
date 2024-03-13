using Dapper.Contrib.Extensions;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_DIA_TRABALHADO_FUNCIONARIO")]
    public class DiaTrabalhadoFuncionario
    {
        [Key]
        public int DtfId { get; set; }

        public int DitId { get; set; }

        public int FunId { get; set; }
    }
}
