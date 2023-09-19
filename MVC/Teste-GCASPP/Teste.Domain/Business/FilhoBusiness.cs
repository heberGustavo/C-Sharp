using Teste.Domain.Business.Base;
using Teste.Domain.IBusiness;
using Teste.Domain.IRepository;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.Domain.Models.Body;

namespace Teste.Domain.Business
{
    public class FilhoBusiness : BusinessBase<Filho>, IFilhoBusiness
    {
        private readonly IFilhoRepository _filhoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FilhoBusiness(IFilhoRepository filhoRepository, IFuncionarioRepository funcionarioRepository) : base(filhoRepository)
        {
            _filhoRepository = filhoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<ResultResponseModel> Cadastrar(Filho model)
        {
            try
            {
                var resultFuncionario = await _funcionarioRepository.GetById(model.id_funcionario);

                if(model.data_de_nascimento <= resultFuncionario.data_de_nascimento)
                {
                    return new ResultResponseModel(true, "Opa, perai! O Depentende nasceu antes do pai!? Algo de errado não está certo! Por favor tente novamente.");
                }

                var result = await _filhoRepository.CreateAsync(model);
                if (result == 0) return new ResultResponseModel(true, "Erro ao cadastrar Dependente. Tente novamente!");

                return new ResultResponseModel(false, "Dependente cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Dependente. Contate o administrador.");
            }
        }

        public async Task<ResultResponseModel> Editar(Filho model)
        {
            try
            {
                var resultFuncionario = await _funcionarioRepository.GetById(model.id_funcionario);

                if (model.data_de_nascimento <= resultFuncionario.data_de_nascimento)
                {
                    return new ResultResponseModel(true, "Opa, perai! O Depentende nasceu antes do pai!? Algo de errado não está certo! Por favor tente novamente.");
                }

                var result = await _filhoRepository.UpdateAsync(model);
                if (result == null) return new ResultResponseModel(true, "Erro ao editar Dependente. Tente novamente.");

                return new ResultResponseModel(false, "Dependente alterado com sucesso!");
            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Ocorreu um erro. Contate o administrador.");
            }
        }

        public Task<ResultResponseModel> Excluir(int id)
            => _filhoRepository.Excluir(id);

        public Task<Filho> ObterFilhoPorId(int id)
            => _filhoRepository.GetById(id);

        public Task<IEnumerable<FilhoBody>> ObterTodosFilhos()
            => _filhoRepository.ObterTodosFilhos();

        public Task<int> VerificaSeExisteFilhoCadastrado(int id)
            => _filhoRepository.VerificaSeExisteFilhoCadastrado(id);
    }
}
