using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IRepository
{
    public interface IFuncionarioRepository : IRepositoryBase<FuncionarioDTO>
    {
        Task<IList<FuncionarioDTO>> ObterCadastrados();
    }
}
