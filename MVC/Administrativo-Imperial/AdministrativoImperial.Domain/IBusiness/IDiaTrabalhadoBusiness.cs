using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IDiaTrabalhadoBusiness : IBusinessBase<DiaTrabalhadoDTO>
    {
        Task<ResultInfo> Cadastrar(DiaTrabalhadoDTO diaTrabalhado);
        Task<ResultInfo<DiaTrabalhadoDTO>> ObterCadastrados();
        Task<ResultInfo<DiaTrabalhadoDTO>> Selecionar(int ditId);
        Task<ResultInfo> Deletar(int ditId);
    }
}
