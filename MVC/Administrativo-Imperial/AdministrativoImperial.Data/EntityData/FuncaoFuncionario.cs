using Dapper.Contrib.Extensions;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("TB_FUNCAO_FUNCIONARIO")]
    public class FuncaoFuncionario
    {
        [Key]
        public int FnfId { get; set; }
        public string FnfNome { get; set; }
    }
}
