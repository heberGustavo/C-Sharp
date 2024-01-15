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
                var verificaUsuario = SelecionarPorEmail(model.UsaEmail).Result;
                if (verificaUsuario.Type != ResultType.CompleteExecution)
                {
                    result.Messages = verificaUsuario.Messages;
                    return result;
                }

                if (verificaUsuario.Item != null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Usuário já cadastrado!");
                    return result;
                }

                var salt = BCryptNet.BCrypt.GenerateSalt();

                var x = BCryptNet.BCrypt.HashPassword(model.senha, salt);

                model.UsaSenha = Encoding.UTF8.GetBytes(BCryptNet.BCrypt.HashPassword(model.senha, salt));
                model.UsaSalt = Encoding.UTF8.GetBytes(salt);
                
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

        public object Deletar(int usaId)
        {
            throw new Exception();
        }

        #endregion

        #region Read

        public async Task<ResultInfo<UsuarioDTO>> SelecionarPorEmail(string email)
        {
            var result = new ResultInfo<UsuarioDTO>();

            try
            {
                result.Item = await _usuarioRepository.ObterUsuarioPorEmail(email);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao selecionar usuário. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
            }

            return result;
        }

        public async Task<ResultInfo<UsuarioDTO>> Listar()
        {
            var result = new ResultInfo<UsuarioDTO>();

            try
            {
                result.Items = await _usuarioRepository.Listar();
            }
            catch (Exception)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao listar obras");
            }

            return result;
        }

        #endregion

    }
}
