using AdministrativoImperial.Common;
using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
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

        public async Task<ResultResponseModel> Cadastrar(ObraDTO obra)
        {
            obra.data_fim = Convert.ToDateTime(DataDictionary.DATE_MIN);

            try
            {
                var idCadastrado = await _obraRepository.CreateAsync(obra);
                if (idCadastrado > 0)
                    return new ResultResponseModel(false, "Obra cadastrada com sucesso!");
                else
                    return new ResultResponseModel(true, "Erro ao cadastrar Obra. Tente novamente!");
            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Obra. Entre em contato com o Administrador.");
            }
        }

        public async Task<IEnumerable<ObraDTO>> ObterCadastrados()
            => await _obraRepository.GetAllAsync();
    }
}
