using AutoMapper;
using Teste.Data.EntityData;
using Teste.Data.Repository.Base;
using Teste.Domain.IRepository;
using Teste.Domain.Models.EntityDomain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Teste.Domain.Models.Body;

namespace Teste.Data.Repository
{
    public class FilhoRepository : RepositoryBase<Filho, FilhoData>, IFilhoRepository
    {
        public FilhoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<ResultResponseModel> Excluir(int id)
        {
            try
            {
                var result = await _dataContext.Connection.ExecuteAsync(@"DELETE FROM TB_FILHO WHERE id = @id;", new { id });

                if (result >= 1)
                    return new ResultResponseModel(false, "Filho excluído com sucesso!");
                else
                    return new ResultResponseModel(true, "Erro ao excluir filho. Tente novamente!");

            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Ocorreu um erro. Contate o administrador.");
            }
        }

        public Task<IEnumerable<FilhoBody>> ObterTodosFilhos()
            => _dataContext.Connection.QueryAsync<FilhoBody>(@"SELECT 
	                                                                fil.id,
	                                                                fil.nome,
	                                                                fil.data_de_nascimento,
	                                                                fil.id_funcionario,
	                                                                fun.nome as nome_funcionario
                                                               FROM 
	                                                               TB_FILHO as fil
                                                               INNER JOIN 
	                                                               TB_FUNCIONARIO fun ON fun.id = fil.id_funcionario
                                                               ORDER BY 
	                                                               fil.nome");

        public async Task<int> VerificaSeExisteFilhoCadastrado(int id)
        {
            var result = await _dataContext.Connection.QueryAsync<Filho>(@"SELECT *
                                                                           FROM TB_FILHO
                                                                           WHERE id_funcionario = @id", new { id });

            if (result.Count() >= 1)
                return 1; //Tem elementos
            else
                return 0; //Lista vazia
        }
    }
}
