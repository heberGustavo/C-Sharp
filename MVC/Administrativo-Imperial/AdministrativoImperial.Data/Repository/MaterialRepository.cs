using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class MaterialRepository : RepositoryBase<MaterialDTO, Material>, IMaterialRepository
    {
        public MaterialRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IList<MaterialDTO>> ObterCadastrados()
        {
            var resultData = await _dataContext.Connection.QueryAsync<MaterialDTO>(@"
                                                                                        SELECT 
	                                                                                        Mtr.MtrId
	                                                                                        , Mtr.ObrId
	                                                                                        , Obr.ObrApelido
                                                                                            , Mtr.MtrNome
	                                                                                        , Mtr.MtrDescricao
	                                                                                        , Mtr.MtrValor
	                                                                                        , Mtr.MtrDataCompra
                                                                                        FROM 
	                                                                                        TB_MATERIAL Mtr 
	                                                                                        INNER JOIN TB_OBRA Obr WITH(NOLOCK) ON Obr.ObrId = Mtr.ObrId
                                                                                        ORDER BY
	                                                                                        MtrDataCompra ASC
                                                                                       ");
            return resultData.ToList();
        }
    }
}
