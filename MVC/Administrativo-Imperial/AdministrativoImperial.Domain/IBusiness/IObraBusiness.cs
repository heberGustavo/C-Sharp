using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IObraBusiness : IBusinessBase<ObraDTO>
    {
        Task<ResultInfo> Cadastrar(ObraDTO obra);
        Task<ResultInfo<ObraDTO>> ObterCadastrados();
        Task<ResultInfo<ObraDTO>> Selecionar(int obrId);
        Task<ResultInfo> Deletar(int obrId);
    }
}
