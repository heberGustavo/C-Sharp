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
																		P.PRO_ID
																	,P.PRO_NOME
																	,P.PRO_DESCRICAO
																	,P.PRO_PRECO
																	,P.PRO_SITUACAO
																	,P.CAT_ID
																FROM 
																	TB_PRODUTO P
																	INNER JOIN TB_CATEGORIA C WITH(NOLOCK) ON C.CAT_ID = P.CAT_ID
																WHERE 
																		P.CAT_ID = @idCategoria
																	AND P.PRO_DESCRICAO LIKE @descricaoProduto
																	AND P.PRO_SITUACAO = @situacao",
                                                                    new { idCategoria, descricaoProduto, situacao });
    }
}
