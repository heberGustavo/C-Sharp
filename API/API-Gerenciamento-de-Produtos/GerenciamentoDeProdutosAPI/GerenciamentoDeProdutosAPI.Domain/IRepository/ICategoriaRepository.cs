using GerenciamentoDeProdutosAPI.Domain.IRepository.Base;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IRepository
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> FindByCategorySituation(string nomeCategoria, bool situacao);
    }
}
