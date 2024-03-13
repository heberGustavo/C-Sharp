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
    public class DiaTrabalhadoFuncionarioRepository : RepositoryBase<DiaTrabalhadoFuncionarioDTO, DiaTrabalhadoFuncionario>, IDiaTrabalhadoFuncionarioRepository
    {
        public DiaTrabalhadoFuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<List<DiaTrabalhadoFuncionarioDTO>> Listar(int ditId)
        {
            var resultData = await _dataContext.Connection.QueryAsync<DiaTrabalhadoFuncionarioDTO>(@"SELECT 
	                                                                                                    Dtf.DtfId
                                                                                                        , Dtf.DitId
	                                                                                                    , Fun.FunId
	                                                                                                    , Fun.FunNome
                                                                                                    FROM 
	                                                                                                    dbo.TB_DIA_TRABALHADO_FUNCIONARIO Dtf
	                                                                                                    INNER JOIN dbo.TB_FUNCIONARIO Fun ON Fun.FunId = Dtf.FunId
                                                                                                    WHERE 
	                                                                                                    Dtf.DitId = @ditId",
                                                                                                        new { ditId });
            return resultData.ToList();
        }

    }
}
