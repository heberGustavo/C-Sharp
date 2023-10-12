using AdministrativoImperial.Common;
using AdministrativoImperial.Domain.Business.Base;
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
    public class ObraBusiness : BusinessBase<ObraDTO>, IObraBusiness
    {
        private readonly IObraRepository _obraRepository;

        public ObraBusiness(IObraRepository obraRepository) : base(obraRepository)
        {
            _obraRepository = obraRepository;
        }

        #region Write

        public async Task<ResultInfo> Cadastrar(ObraDTO obra)
        {
            var result = new ResultInfo();

            try
            {
                if (obra.ObrId <= 0)
                    result = await Inserir(obra);
                else
                    result = await Alterar(obra);

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao salvar dados.");
            }

            return result;
        }

        public async Task<ResultInfo> Deletar(int obrId)
        {
            var result = new ResultInfo();

            try
            {
                var modelDeletar = await _obraRepository.GetById(obrId);
                if(modelDeletar == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao selecionar obra. Tente novamente!");
                }

                await _obraRepository.DeleteAsync(modelDeletar);
                result.Messages.Add("Obra deletada com sucesso!");
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao deletar obra");
            }
            return result;
        }

        #region Métodos privados

        private async Task<ResultInfo> Inserir(ObraDTO obra)
        {
            var result = new ResultInfo();

            try
            {
                obra.ObrDataFim = Convert.ToDateTime(DataDictionary.DATE_MIN);

                var idCadastrado = await _obraRepository.CreateAsync(obra);
                if (idCadastrado > 0)
                    result.Messages.Add("Obra cadastrada com sucesso!");
                else
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar obra. Tente novamente!");
                }
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar obra.");
            }

            return result;
        }

        private async Task<ResultInfo> Alterar(ObraDTO obra)
        {
            var result = new ResultInfo();

            try
            {
                obra.ObrDataFim = Convert.ToDateTime(DataDictionary.DATE_MIN);

                var modelAtualizada = await _obraRepository.UpdateAsync(obra);
                if (modelAtualizada != null)
                    result.Messages.Add("Obra atualizada com sucesso!");
                else
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao atualizada obra. Tente novamente!");
                }
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao atualizada obra.");
            }

            return result;
        }

        #endregion

        #endregion

        #region Read

        public async Task<ResultInfo<ObraDTO>> ObterCadastrados()
        {
            var result = new ResultInfo<ObraDTO>();

            try
            {
                result.Items = await _obraRepository.Listar();
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao listar obras");
            }

            return result;
        }

        public async Task<ResultInfo<ObraDTO>> Selecionar(int obrId)
        {
            var result = new ResultInfo<ObraDTO>();

            try
            {
                result.Item = await _obraRepository.GetById(obrId);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao selecionar obra");
            }

            return result;
        }

        #endregion

    }
}
