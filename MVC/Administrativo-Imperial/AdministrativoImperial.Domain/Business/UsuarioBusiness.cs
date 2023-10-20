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
using BCryptNet = BCrypt.Net;

namespace AdministrativoImperial.Domain.Business
{
    public class UsuarioBusiness : BusinessBase<UsuarioDTO>, IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #region Writer

        public async Task<ResultInfo> Create(UsuarioDTO model)
        {
            var result = new ResultInfo();

            try
            {
                var salt = BCryptNet.BCrypt.GenerateSalt();
                var senhaHash = BCryptNet.BCrypt.HashPassword(model.UsaSenha, salt);
                
                model.UsaSenha = senhaHash;
                model.UsaSalt = salt;

                var idResult = await _usuarioRepository.CreateAsync(model);
                if (idResult <= 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Usuário. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Usuário cadastrado com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Usuário. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

        #endregion

        #region Read

        public async Task<ResultInfo<UsuarioDTO>> Selecionar(string email, string senha)
        {
            var result = new ResultInfo<UsuarioDTO>();

            try
            {
                result.Item = await _usuarioRepository.ObterUsuario(email, senha);
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
