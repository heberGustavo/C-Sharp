using Dapper.Contrib.Extensions;

namespace AdministrativoImperial.Data.EntityData
{
    [Table("FuncaoFuncionario")]
    public class FuncaoFuncionarioData
    {
        [Key]
        public int id_funcao_funcionario { get; set; }
        public string nome { get; set; }
        public bool excluido { get; set; }
    }
}
