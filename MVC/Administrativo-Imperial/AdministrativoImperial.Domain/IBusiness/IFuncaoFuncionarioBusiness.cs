using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IFuncaoFuncionarioBusiness : IBusinessBase<FuncaoFuncionarioDTO>
    {
        Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastrados();
        Task<ResultResponseModel> Cadastrar(FuncaoFuncionarioDTO funcaoFuncionario);
        Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastradosAtivos();
        Task<ResultResponseModel> Deletar(int id);
    }
}
