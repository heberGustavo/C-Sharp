﻿using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IObraBusiness : IBusinessBase<ObraDTO>
    {
        Task<ResultResponseModel> Cadastrar(ObraDTO obra);
        Task<ResultInfo<ObraDTO>> ObterCadastrados();
    }
}
