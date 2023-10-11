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
    public interface IMaterialBusiness : IBusinessBase<MaterialDTO>
    {
        Task<ResultInfo<MaterialDTO>> GetAllAsync();
        Task<ResultInfo> Create(MaterialDTO model);
        Task<IEnumerable<MaterialDTO>> ObterCadastradosAtivos();
        Task<ResultInfo> Deletar(int id);
        Task<ResultInfo<MaterialDTO>> Selecionar(int mtrId);
    }
}
