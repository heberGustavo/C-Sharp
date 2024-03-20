using Effortless.Net.Encryption;
using Sorteio.Common;
using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class UsuarioBusiness : BusinessBase<Usuario>, IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISorteiosRepository _sorteiosRepository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, ISorteiosRepository sorteiosRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _sorteiosRepository = sorteiosRepository;
        }

        public async Task<ResultResponseModel<Usuario>> CadastrarUsuarioCadastrarNumeros(CadastrarUsuarioListaNumerosBody body)
        {
            var idUsuarioCadastrado = 0;
            var usuarioCadastrado = true;

            var usuarios = await _usuarioRepository.GetAllAsync(x => x.id_tipo_usuario != DataDictionary.USUARIO_ADMINISTRADOR);
            body.usuario.id_tipo_usuario = DataDictionary.USUARIO_CLIENTE;

            if (usuarios.Count <= 0)
            {
                usuarioCadastrado = false;
            }
            else
            {
                foreach (var item in usuarios)
                {
                    if (item.celular.Equals(body.usuario.celular, StringComparison.OrdinalIgnoreCase))
                    {
                        body.usuario.id_usuario_cadastrado = item.id_usuario;
                        body.usuario.id_usuario = item.id_usuario;
                        usuarioCadastrado = true;

                        var cadastrarNumeros = await _sorteiosRepository.CadastrarNumerosEscolhidos(body.valor_total, body.numeroSorteios, item.id_usuario, body.id_sorteio);

                        if (cadastrarNumeros)
                            return new ResultResponseModel<Usuario>(false, "Cadastro realizado com sucesso", item.id_usuario, body.usuario);
                        else
                            return new ResultResponseModel<Usuario>(true, "Infelizmente não conseguimos cadastrar seus números. Tente novamente!", null);

                    }
                    else
                    {
                        usuarioCadastrado = false;
                    }
                }

            }

            if (!usuarioCadastrado)
            {
                idUsuarioCadastrado = await _usuarioRepository.CreateAsync(body.usuario);
            }

            body.usuario.id_usuario_cadastrado = idUsuarioCadastrado;

            if (idUsuarioCadastrado == 0) return new ResultResponseModel<Usuario>(true, "Erro ao cadastrar usuário", null);

            var cadastrarNumerosEscolhidos = await _sorteiosRepository.CadastrarNumerosEscolhidos(body.valor_total, body.numeroSorteios, idUsuarioCadastrado, body.id_sorteio);

            if (cadastrarNumerosEscolhidos)
                return new ResultResponseModel<Usuario>(false, "Cadastro realizado com sucesso", idUsuarioCadastrado, body.usuario);
            else
                return new ResultResponseModel<Usuario>(true, "Infelizmente não conseguimos cadastrar seus números. Tente novamente!", null);



        }

        public Task<IEnumerable<NumeroEscolhidoBody>> ConsultarNumerosCliente(string celularUsuario, int idSorteio)
            => _usuarioRepository.ConsultarNumerosCliente(celularUsuario, idSorteio);

        public async Task<ResultResponseModel> CriarUsuario(Usuario usuario)
        {
            usuario.senha = Hash.Create(HashType.SHA256, usuario.senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var idUsuarioCadastrado = await _usuarioRepository.CreateAsync(usuario);

            if (idUsuarioCadastrado == 0) return new ResultResponseModel(true, "Erro ao cadastrar usuário");

            return new ResultResponseModel(false, "Cadastro realizado com sucesso");
        }

        public Task<Usuario> InformacoesDoUsuarioGanhadorPorId(int idUsuario)
            => _usuarioRepository.GetById(idUsuario);

        public async Task<ResultResponseModel<Usuario>> LogarCadastraNumeros(LoginListaNumerosBody login)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            var usuarioCadastrado = usuarios.FirstOrDefault(u => u.celular == login.celular);

            if (usuarioCadastrado == null) return new ResultResponseModel<Usuario>(true, "Login/Senha Inválidos", null);

            var cadastrarNumerosEscolhidos = await _sorteiosRepository.CadastrarNumerosEscolhidos(login.valor_total, login.numeroSorteios, usuarioCadastrado.id_usuario, login.id_sorteio);


            return new ResultResponseModel<Usuario>(false, "Sucesso", usuarioCadastrado);
        }

        public Task<IEnumerable<NumeroEscolhidoBody>> MostrarNumerosDoUsuario(int idUsuario)
            => _usuarioRepository.MostrarNumerosDoUsuario(idUsuario);

        public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
            => _usuarioRepository.GetAllAsync();

        public async Task<ResultResponseModel<Usuario>> RealizarLogin(string email, string senha)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            senha = Hash.Create(HashType.SHA256, senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var usuarioCadastrado = usuarios.FirstOrDefault(u => u.email == email && u.senha == senha);

            if (usuarioCadastrado == null) return new ResultResponseModel<Usuario>(true, "Login/Senha Inválidos", null);

            return new ResultResponseModel<Usuario>(false, "Sucesso", usuarioCadastrado);
        }
    }
}