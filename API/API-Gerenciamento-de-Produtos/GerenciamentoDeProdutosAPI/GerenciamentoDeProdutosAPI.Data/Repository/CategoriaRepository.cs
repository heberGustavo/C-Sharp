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
    public class CategoriaRepository : RepositoryBase<Categoria, CategoriaData>, ICategoriaRepository
    {
        public CategoriaRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IEnumerable<Categoria>> FindByCategorySituation(string nomeCategoria, bool situacao)
            => await _dataContext.Connection.QueryAsync<Categoria>(@"
                                                                    SELECT 
	                                                                      ID
	                                                                    , NOME
	                                                                    , SITUACAO
                                                                    FROM 
	                                                                    TB_CATEGORIA
                                                                    WHERE 
	                                                                    NOME LIKE @nomeCategoria
	                                                                    AND SITUACAO = @situacao", 
                                                                    new { nomeCategoria, situacao });
       
    }
}
