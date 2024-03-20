using Sorteio.Domain.IBusiness.Base;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IBusiness
{
    public interface ICategoriaSorteioBusiness : IBusinessBase<CategoriaSorteio>
    {
        Task<ResultResponseModel> CriarCategoriaSorteio(CategoriaSorteio categoriaSorteio);
        Task<IEnumerable<CategoriaSorteio>> ObterTodosCategoriaSorteioAtivo();
        Task<ResultResponseModel> EditarCategoriaSorteio(CategoriaSorteio categoriaSorteio);
        Task<int> ExcluirCategoriaSorteio(int idCategoriaSorteio);
    }
}
