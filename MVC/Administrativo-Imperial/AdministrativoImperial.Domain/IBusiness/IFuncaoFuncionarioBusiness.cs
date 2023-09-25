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
    public interface IFuncaoFuncionarioBusiness : IBusinessBase<FuncaoFuncionarioDTO>
    {
        Task<ResultInfo<FuncaoFuncionarioDTO>> GetAllAsync();
        Task<ResultInfo> Create(FuncaoFuncionarioDTO funcaoFuncionario);
        Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastradosAtivos();
        Task<ResultInfo> Deletar(int id);
    }
}
