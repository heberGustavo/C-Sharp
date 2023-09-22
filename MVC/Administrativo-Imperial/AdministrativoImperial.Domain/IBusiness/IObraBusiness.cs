using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IObraBusiness : IBusinessBase<ObraDTO>
    {
        Task<ResultResponseModel> Cadastrar(ObraDTO obra);
        Task<IEnumerable<ObraDTO>> ObterCadastrados();
    }
}
