using AdministrativoImperial.Common;
using AdministrativoImperial.Common.Helpers;
using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
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

        public async Task<ResultInfo> Cadastrar(UsuarioDTO usuario)
        {
            var result = new ResultInfo();

            try
            {
                if (usuario.UsaId <= 0)
                    result = await Create(usuario);
                else
                    result = await Update(usuario);
            }
            catch (Exception)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add(Mensagens.MENSAGEM_ERRO_INESPERADO);
                return result;
            }

            return result;

        }

        public async Task<ResultInfo> Deletar(int usaId)
        {
            var result = new ResultInfo();

            try
            {
                var modelDeletar = await _usuarioRepository.GetById(usaId);
                if (modelDeletar == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add($"Erro ao selecionar {NomeTela.Usuario}. Tente novamente!");
                }

                await _usuarioRepository.DeleteAsync(modelDeletar);
                result.Messages.Add($"{NomeTela.Usuario} deletado com sucesso!");
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add($"Erro ao deletar {NomeTela.Usuario}. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
            }

            return result;

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
                result.Messages.Add($"Erro ao selecionar {NomeTela.Usuario}. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
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
                result.Messages.Add($"Erro ao listar {NomeTela.Usuario}s. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
            }

            return result;
        }

        public async Task<ResultInfo<UsuarioDTO>> Selecionar(int usaId)
        {
            var result = new ResultInfo<UsuarioDTO>();
            
            try
            {
                result.Item = await _usuarioRepository.GetById(usaId);
            }
            catch (Exception)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add($"Erro ao selecionar {NomeTela.Usuario}. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
            }

            return result;
        }

        #endregion

        #region Private Methods

        private async Task<ResultInfo> Create(UsuarioDTO model)
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
                result.Messages.Add($"Erro ao cadastrar {NomeTela.Usuario}. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
                return result;
            }

            return result;
        }

        private async Task<ResultInfo> Update(UsuarioDTO model)
        {
            var result = new ResultInfo();

            try
            {
                var selecionaUsuario = await _usuarioRepository.GetById(model.UsaId);
                if (selecionaUsuario == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add($"Erro ao validar {NomeTela.Usuario}. Tente novamente!");
                    return result;
                }

                model.UsaSalt = selecionaUsuario.UsaSalt;
                model.UsaSenha = selecionaUsuario.UsaSenha;
                var modelAtualizada = await _usuarioRepository.UpdateAsync(model);
                if (modelAtualizada == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add($"Erro ao atualizar {NomeTela.Usuario}. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Usuário atualizado com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add($"Erro ao atualizar {NomeTela.Usuario}. " + Mensagens.MENSAGEM_CONTATO_ADMINISTRADOR);
                return result;
            }

            return result;
        }

        #endregion
    }
}
