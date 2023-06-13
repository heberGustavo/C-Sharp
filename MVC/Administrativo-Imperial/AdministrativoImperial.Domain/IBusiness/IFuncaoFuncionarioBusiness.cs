using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IFuncaoFuncionarioBusiness : IBusinessBase<FuncaoFuncionario>
    {
        Task<IEnumerable<FuncaoFuncionario>> ObterCadastrados();
        Task<ResultResponseModel> Cadastrar(FuncaoFuncionario funcaoFuncionario);
        Task<IEnumerable<FuncaoFuncionario>> ObterCadastradosAtivos();
        Task<ResultResponseModel> Deletar(int id);
    }
}
