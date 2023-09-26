﻿using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Business
{
    public class FuncionarioBusiness : BusinessBase<FuncionarioDTO>, IFuncionarioBusiness
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioBusiness(IFuncionarioRepository funcionarioRepository) : base(funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<ResultResponseModel> Cadastrar(FuncionarioDTO funcioanrio)
        {
            try
            {
                var idCadastrado = await _funcionarioRepository.CreateAsync(funcioanrio);
                if (idCadastrado > 0)
                    return new ResultResponseModel(false, "Funcionário cadastrado com sucesso!");
                else
                    return new ResultResponseModel(true, "Erro ao cadastrar Funcionário. Tente novamente!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Funcionário. Entre em contato com o Administrador.");
            }
        }

        public async Task<ResultInfo<FuncionarioDTO>> ObterCadastrados()
        {
            var result = new ResultInfo<FuncionarioDTO>();

            try
            {
                result.Items = await _funcionarioRepository.ObterCadastrados();
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao obter dados dos Funcionários");
            }

            return result;
        }

    }
}
