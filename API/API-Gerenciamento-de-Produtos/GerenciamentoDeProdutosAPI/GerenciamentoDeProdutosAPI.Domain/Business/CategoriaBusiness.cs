using GerenciamentoDeProdutosAPI.Domain.Business.Base;
using GerenciamentoDeProdutosAPI.Domain.IBusiness;
using GerenciamentoDeProdutosAPI.Domain.IRepository;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.Business
{
    public class CategoriaBusiness : BusinessBase<CategoriaDTO>, ICategoriaBusiness
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaBusiness(ICategoriaRepository categoriaRepository) : base(categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        #region Read

        public Task<IEnumerable<CategoriaDTO>> FindAll()
            => _categoriaRepository.GetAllAsync();

        public Task<IEnumerable<CategoriaDTO>> FindByCategorySituation(string nomeCategoria, bool situacao)
            => _categoriaRepository.FindByCategorySituation(nomeCategoria, situacao);

        public Task<CategoriaDTO> FindById(int id)
            => _categoriaRepository.GetById(id);

        #endregion

        #region Write

        public Task<int> Create(CategoriaDTO categoria)
        {
            var result = _categoriaRepository.CreateAsync(categoria);
            return result;
        }

        public Task<CategoriaDTO> Update(CategoriaDTO categoria)
        {
            var result = _categoriaRepository.UpdateAsync(categoria);
            return result;
        }

        #endregion

    }
}
