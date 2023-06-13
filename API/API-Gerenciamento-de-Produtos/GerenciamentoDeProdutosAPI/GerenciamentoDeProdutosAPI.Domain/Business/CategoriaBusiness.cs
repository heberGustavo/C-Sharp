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
    public class CategoriaBusiness : BusinessBase<Categoria>, ICategoriaBusiness
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaBusiness(ICategoriaRepository categoriaRepository) : base(categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        #region Read

        public Task<IEnumerable<Categoria>> FindAll()
            => _categoriaRepository.GetAllAsync();

        public Task<IEnumerable<Categoria>> FindByCategorySituation(string nomeCategoria, bool situacao)
            => _categoriaRepository.FindByCategorySituation(nomeCategoria, situacao);

        public Task<Categoria> FindById(int id)
            => _categoriaRepository.GetById(id);

        #endregion

        #region Write

        public Task<int> Create(Categoria categoria)
        {
            var result = _categoriaRepository.CreateAsync(categoria);
            return result;
        }

        public Task<Categoria> Update(Categoria categoria)
        {
            var result = _categoriaRepository.UpdateAsync(categoria);
            return result;
        }

        #endregion

    }
}
