using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.Business
{
    public class FilhoBusiness : BusinessBase<Filho>, IFilhoBusiness
    {
        private readonly IFilhoRepository _filhoRepository;

        public FilhoBusiness(IFilhoRepository filhoRepository) : base(filhoRepository)
        {
            _filhoRepository = filhoRepository;
        }

        public async Task<ResultResponseModel> Cadastrar(Filho model)
        {
            try
            {
                var result = await _filhoRepository.CreateAsync(model);
                if (result == 0) return new ResultResponseModel(true, "Erro ao cadastrar Filho. Tente novamente!");

                return new ResultResponseModel(false, "Filho cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Filho. Contate o administrador.");
            }
        }

        public async Task<ResultResponseModel> Editar(Filho model)
        {
            try
            {
                var result = await _filhoRepository.UpdateAsync(model);
                if (result == null) return new ResultResponseModel(true, "Erro ao editar Filho. Tente novamente.");

                return new ResultResponseModel(false, "Filho alterado com sucesso!");
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
