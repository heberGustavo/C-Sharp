using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class FuncionarioRepository : RepositoryBase<Funcionario, FuncionarioData>, IFuncionarioRepository
    {
        public FuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public Task<IEnumerable<Funcionario>> ObterCadastrados()
            => _dataContext.Connection.QueryAsync<Funcionario>(@"SELECT f.*, ff.nome as nome_funcao
                                                                 FROM Funcionario f
                                                                 INNER JOIN FuncaoFuncionario ff ON ff.id_funcao_funcionario = f.id_funcao_funcionario
                                                                 ORDER BY f.excluido asc, f.nome");
    }
}
