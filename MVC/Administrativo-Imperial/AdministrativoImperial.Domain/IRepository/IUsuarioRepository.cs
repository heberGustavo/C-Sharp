using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IRepository
{
    public interface IUsuarioRepository : IRepositoryBase<UsuarioDTO>
    {
        Task<IList<UsuarioDTO>> Listar();
        Task<UsuarioDTO> ObterUsuarioPorEmail(string email);
    }
}
