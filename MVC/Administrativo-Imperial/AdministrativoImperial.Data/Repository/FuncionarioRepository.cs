using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class FuncionarioRepository : RepositoryBase<FuncionarioDTO, Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IList<FuncionarioDTO>> ObterCadastrados()
        {
            var resultData = await _dataContext.Connection.QueryAsync<FuncionarioDTO>(@"
                                                                                        SELECT 
	                                                                                          Fun.FunId
	                                                                                        , Fun.FunNome
	                                                                                        , Fun.FunDiaria
	                                                                                        , Fun.FunMensal
	                                                                                        , Fun.FunDataContratacao
	                                                                                        , Fun.FunStatus
	                                                                                        , Fnf.FnfNome as NomeFuncao
                                                                                        FROM 
	                                                                                        TB_FUNCIONARIO Fun
	                                                                                        INNER JOIN TB_FUNCAO_FUNCIONARIO Fnf ON Fnf.FnfId = Fun.FnfId
                                                                                        ORDER BY 
	                                                                                        Fun.FunStatus, Fun.FunNome
                                                                                       ");
            return resultData.ToList();
        }

    }
}
