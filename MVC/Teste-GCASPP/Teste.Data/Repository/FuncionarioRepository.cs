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

namespace Teste.Data.Repository
{
    public class FuncionarioRepository : RepositoryBase<Funcionario, FuncionarioData>, IFuncionarioRepository
    {
        public FuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public Task<IEnumerable<Funcionario>> ObterTodosFuncionarios()
            => _dataContext.Connection.QueryAsync<Funcionario>(@"SELECT *
                                                                 FROM TB_FUNCIONARIO
                                                                 ORDER BY nome, salario");

        public async Task<int> VerificaSeExisteCadastrado(string nome, decimal salario)
        {
            var result = await _dataContext.Connection.QueryAsync<Funcionario>(@"SELECT *
                                                                                 FROM TB_FUNCIONARIO
                                                                                 WHERE 
	                                                                                 nome = @nome AND
	                                                                                 salario = @salario", new { nome, salario });
            
            if (result.Count() >= 1 )
                return 1; //Tem elementos
            else
                return 0; //Lista vazia
                
        }

        public async Task<ResultResponseModel> Excluir(int id)
        {
            try
            {
                var result = await _dataContext.Connection.ExecuteAsync(@"DELETE FROM TB_FUNCIONARIO WHERE id = @id;", new { id });

                if (result >= 1)
                    return new ResultResponseModel(false, "Funcionário excluído com sucesso!");
                else
                    return new ResultResponseModel(true, "Erro ao excluir Funcionário. Tente novamente!");

            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Ocorreu um erro. Contate o administrador.");
            }
        }

    }
}
