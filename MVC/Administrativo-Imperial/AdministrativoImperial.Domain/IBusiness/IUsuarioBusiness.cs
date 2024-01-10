using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IUsuarioBusiness : IBusinessBase<UsuarioDTO>
    {
        Task<ResultInfo> Create(UsuarioDTO usuario);

        Task<ResultInfo<UsuarioDTO>> SelecionarPorEmail(string usaEmail);
    }
}
