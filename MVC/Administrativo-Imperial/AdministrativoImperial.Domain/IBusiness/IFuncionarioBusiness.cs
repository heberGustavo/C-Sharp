﻿using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IBusiness
{
    public interface IFuncionarioBusiness : IBusinessBase<FuncionarioDTO>
    {
        Task<IEnumerable<FuncionarioDTO>> ObterCadastrados();
        Task<ResultResponseModel> Cadastrar(FuncionarioDTO funcioanrio);
    }
}
