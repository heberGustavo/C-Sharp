using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class CategoriaSorteioBusiness : BusinessBase<CategoriaSorteio>, ICategoriaSorteioBusiness
    {
        private readonly ICategoriaSorteioRepository _categoriaSorteioRepository;

        public CategoriaSorteioBusiness(ICategoriaSorteioRepository categoriaSorteioRepository) : base(categoriaSorteioRepository)
        {
            _categoriaSorteioRepository = categoriaSorteioRepository;
        }

        public async Task<ResultResponseModel> CriarCategoriaSorteio(CategoriaSorteio categoriaSorteio)
        {
            try
            {
                var categoriaExistenteAtivo = await _categoriaSorteioRepository.GetAllAsync(cs => cs.status == false);

                foreach(var item in categoriaExistenteAtivo)
                {
                    if(item.nome.Equals(categoriaSorteio.nome, StringComparison.OrdinalIgnoreCase))
                    {
                        return new ResultResponseModel(true, "Categoria já cadastrada");
                    }
                }

                var idCategoriaSorteio = await _categoriaSorteioRepository.CreateAsync(categoriaSorteio);
                if (idCategoriaSorteio == 0) return new ResultResponseModel(true, "Erro ao cadastrar categoria");

                return new ResultResponseModel(false, "Cadastro realizado com sucesso");

            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar categoria");
            }
        }

        public async Task<ResultResponseModel> EditarCategoriaSorteio(CategoriaSorteio categoriaSorteio)
        {
            try
            {
                var categoriaExistenteAtivo = await _categoriaSorteioRepository.GetAllAsync(cs => cs.status == false);

                foreach (var item in categoriaExistenteAtivo)
                {
                    if (item.nome.Equals(categoriaSorteio.nome, StringComparison.OrdinalIgnoreCase))
                    {
                        return new ResultResponseModel(true, "Categoria já cadastrada");
                    }
                }

                await _categoriaSorteioRepository.UpdateAsync(categoriaSorteio);
                return new ResultResponseModel(false, "Categoria atualizada com sucesso!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(false, "Erro ao atualizar categoria. Tente novamente!");
            }
        }

        public Task<int> ExcluirCategoriaSorteio(int idCategoriaSorteio)
            => _categoriaSorteioRepository.ExcluirCategoriaSorteio(idCategoriaSorteio);

        public async Task<IEnumerable<CategoriaSorteio>> ObterTodosCategoriaSorteioAtivo()
            => await _categoriaSorteioRepository.GetAllAsync(cs => cs.status == false);
    }
}
