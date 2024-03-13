using AdministrativoImperial.Common;
using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdministrativoImperial.Domain.Business
{
    public class DiaTrabalhadoBusiness : BusinessBase<DiaTrabalhadoDTO>, IDiaTrabalhadoBusiness
    {
        private readonly IDiaTrabalhadoRepository _dao;
        private readonly IDiaTrabalhadoFuncionarioRepository _diaTrabalhadofuncionarioRepository;

        public DiaTrabalhadoBusiness(IDiaTrabalhadoRepository dao, IDiaTrabalhadoFuncionarioRepository diaTrabalhadiFuncionarioRepository) : base(dao)
        {
            _dao = dao;
            _diaTrabalhadofuncionarioRepository = diaTrabalhadiFuncionarioRepository;
        }

        #region Write

        public async Task<ResultInfo> Cadastrar(DiaTrabalhadoDTO diaTrabalhado)
        {
            var result = new ResultInfo();

            try
            {
                if (diaTrabalhado.DitId <= 0)
                    result = await Inserir(diaTrabalhado);
                //else
                //    result = await Alterar(diaTrabalhado);

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao salvar dados.");
            }

            return result;
        }

        public async Task<ResultInfo> Deletar(int ditId)
        {
            var result = new ResultInfo();

            try
            {
                var listaFuncionarios = await _diaTrabalhadofuncionarioRepository.Listar(ditId);

                foreach (var item in listaFuncionarios)
                {
                    await _diaTrabalhadofuncionarioRepository.DeleteAsync(item);
                }

                var modelDeletar = await _dao.GetById(ditId);
                if(modelDeletar == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao selecionar Dia Trabalhado. Tente novamente!");
                }

                await _dao.DeleteAsync(modelDeletar);
                result.Messages.Add("Dia Trabalhado deletada com sucesso!");
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao deletar Dia Trabalhado");
            }
            return result;
        }

        #region Métodos privados

        private async Task<ResultInfo> Inserir(DiaTrabalhadoDTO diaTrabalhado)
        {
            var result = new ResultInfo();

            try
            {
                #region Validar FunIds

                if (diaTrabalhado.FunIds == null || diaTrabalhado.FunIds.Length == 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao validar Funcionários. Tente novamente!");
                    return result;
                }

                #endregion

                #region Cadastrar Dia Trabalhado

                var idCadastrado = await _dao.CreateAsync(diaTrabalhado);
                if (idCadastrado <= 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Dia Trabalhado. Tente novamente!");
                }

                #endregion

                #region Cadastrar Dia Trabalhado Funcionário

                foreach (var item in diaTrabalhado.FunIds)
                {
                    var idItem = await _diaTrabalhadofuncionarioRepository.CreateAsync(new DiaTrabalhadoFuncionarioDTO { DitId = idCadastrado, FunId = Convert.ToInt32(item) });
                    if (idItem <= 0)
                    {
                        result.Type = ResultType.ValidationError;
                        result.Messages.Add("Erro ao vincular funcionários. Tente novamente!");
                    }
                }

                #endregion

                result.Messages.Add("Dia Trabalhado cadastrada com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Dia Trabalhado.");
            }

            return result;
        }

        #endregion

        #endregion

        #region Read

        public async Task<ResultInfo<DiaTrabalhadoDTO>> ObterCadastrados()
        {
            var result = new ResultInfo<DiaTrabalhadoDTO>();

            try
            {
                var listaHelper = new List<DiaTrabalhadoDTO>();
                var listaDiaTrabalhado = await _dao.Listar();

                if (listaDiaTrabalhado.Count > 0)
                {
                    foreach (var item in listaDiaTrabalhado)
                    {
                        item.DiaTrabalhadoFuncionarios = await _diaTrabalhadofuncionarioRepository.Listar(item.DitId);
                        listaHelper.Add(item);
                    }
                }

                result.Items = listaHelper;

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao listar obras");
            }

            return result;
        }

        public async Task<ResultInfo<DiaTrabalhadoDTO>> Selecionar(int ditId)
        {
            var result = new ResultInfo<DiaTrabalhadoDTO>();

            try
            {
                if(ditId == 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao selecionar item");
                    return result;
                }

                result.Item = await _dao.GetById(ditId);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao selecionar Dia Trabalhado");
            }

            return result;
        }

        #endregion

    }
}
