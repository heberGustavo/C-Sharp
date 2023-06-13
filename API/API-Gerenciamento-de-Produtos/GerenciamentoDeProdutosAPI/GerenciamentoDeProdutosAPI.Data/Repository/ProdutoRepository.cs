using AutoMapper;
using Dapper;
using GerenciamentoDeProdutosAPI.Data.EntityData;
using GerenciamentoDeProdutosAPI.Data.Repository.Base;
using GerenciamentoDeProdutosAPI.Domain.Business.Base;
using GerenciamentoDeProdutosAPI.Domain.IRepository;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Data.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto, ProdutoData>, IProdutoRepository
    {
        public ProdutoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IEnumerable<Produto>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao)
            => await _dataContext.Connection.QueryAsync<Produto>(@"
																SELECT 
																		P.ID
																	,P.NOME
																	,P.DESCRICAO
																	,P.PRECO
																	,P.SITUACAO
																	,P.CAT_ID
																FROM 
																	TB_PRODUTO P
																	INNER JOIN TB_CATEGORIA C WITH(NOLOCK) ON C.ID = P.CAT_ID
																WHERE 
																		P.CAT_ID = @idCategoria
																	AND P.DESCRICAO LIKE @descricaoProduto
																	AND P.SITUACAO = @situacao",
                                                                    new { idCategoria, descricaoProduto, situacao });
    }
}
