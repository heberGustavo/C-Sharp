using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IFuncionarioBusiness : IBusinessBase<FuncionarioDTO>
    {
        Task<ResultInfo<FuncionarioDTO>> ObterCadastrados();
        Task<ResultInfo<FuncionarioDTO>> Selecionar(int funId);
        Task<ResultInfo> Cadastrar(FuncionarioDTO funcioanrio);
        Task<ResultInfo> Desativar(int funId);
    }
}
