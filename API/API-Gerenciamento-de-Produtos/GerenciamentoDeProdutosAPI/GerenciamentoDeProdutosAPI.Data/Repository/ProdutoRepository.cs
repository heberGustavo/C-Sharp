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
    public class ProdutoRepository : RepositoryBase<ProdutoDTO, ProdutoData>, IProdutoRepository
    {
        public ProdutoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IEnumerable<ProdutoDTO>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao)
            => await _dataContext.Connection.QueryAsync<ProdutoDTO>(@"
																SELECT 
																		P.ProId
																	,P.ProNome
																	,P.ProDescricao
																	,P.ProPreco
																	,P.ProSituacao
																	,P.CatId
																FROM 
																	TB_PRODUTO P
																	INNER JOIN TB_CATEGORIA C WITH(NOLOCK) ON C.CatId = P.CatId
																WHERE 
																		P.CatId = @idCategoria
																	AND P.ProDescricao LIKE @descricaoProduto
																	AND P.ProSituacao = @situacao",
                                                                    new { idCategoria, descricaoProduto, situacao });
    }
}
