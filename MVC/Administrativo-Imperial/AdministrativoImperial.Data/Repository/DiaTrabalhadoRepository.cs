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
    public class DiaTrabalhadoRepository : RepositoryBase<DiaTrabalhadoDTO, DiaTrabalhado>, IDiaTrabalhadoRepository
    {
        public DiaTrabalhadoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IList<DiaTrabalhadoDTO>> Listar()
        {
            var resultData = await _dataContext.Connection.QueryAsync<DiaTrabalhadoDTO>(@"SELECT 
	                                                                                        Dit.DitId
	                                                                                        , Dit.DitData
	                                                                                        , Obr.ObrId
	                                                                                        , Obr.ObrApelido
                                                                                        FROM 
	                                                                                        dbo.TB_DIA_TRABALHADO Dit
	                                                                                        INNER JOIN dbo.TB_OBRA Obr ON Obr.ObrId = Dit.ObrId
                                                                                        ORDER BY 
	                                                                                        Dit.DitData DESC");
            return resultData.ToList();
        }
    }
}
