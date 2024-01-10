using AutoMapper;
using Dapper;
using GerenciamentoDeProdutosAPI.Data.EntityData;
using GerenciamentoDeProdutosAPI.Data.Repository.Base;
using GerenciamentoDeProdutosAPI.Domain.IRepository;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Data.Repository
{
    public class CategoriaRepository : RepositoryBase<CategoriaDTO, CategoriaData>, ICategoriaRepository
    {
        public CategoriaRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IEnumerable<CategoriaDTO>> FindByCategorySituation(string nomeCategoria, bool situacao)
            => await _dataContext.Connection.QueryAsync<CategoriaDTO>(@"
                                                                    SELECT 
	                                                                      CatId
	                                                                    , CatNome
	                                                                    , CatSituacao
                                                                    FROM 
	                                                                    TB_CATEGORIA
                                                                    WHERE 
	                                                                    CatNome LIKE @nomeCategoria
	                                                                    AND CatSituacao = @situacao", 
                                                                    new { nomeCategoria, situacao });
       
    }
}
