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
    public class ObraRepository : RepositoryBase<ObraDTO, Obra>, IObraRepository
    {
        public ObraRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IList<ObraDTO>> Listar()
        {
            var resultData = await _dataContext.Connection.
				QueryAsync<ObraDTO>(@"
					DECLARE @BASE_DIAS INT = 25

					SELECT
						OBR.ObrId
						, OBR.ObrApelido
						, OBR.ObrDataInicio
						, OBR.ObrDataFim
						, OBR.ObrEndereco
						, OBR.ObrOrcamento
						, OBR.ObrStatus
						, (
						SELECT 
							SUM(CASE 
								WHEN _FUN.FunMensal <= 0 THEN _FUN.FunDiaria 
								ELSE (_FUN.FunMensal / @BASE_DIAS) END) AS TOTAL
							FROM TB_DIA_TRABALHADO _DIT 
							INNER JOIN TB_DIA_TRABALHADO_FUNCIONARIO _DTF ON _DTF.DitId = _DIT.DitId
							INNER JOIN TB_FUNCIONARIO _FUN ON _FUN.FunId = _DTF.FunId
							WHERE _DIT.ObrId = OBR.ObrId
						) AS GastosTotais
					FROM
						TB_OBRA OBR 
					ORDER BY 
						ObrStatus, ObrApelido
				");
            return resultData.ToList();
        }
    }
}
