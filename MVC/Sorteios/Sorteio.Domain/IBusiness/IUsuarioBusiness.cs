using Sorteio.Domain.IBusiness.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IBusiness
{
    public interface IUsuarioBusiness : IBusinessBase<Usuario>
    {
        Task<ResultResponseModel<Usuario>> RealizarLogin(string email, string senha);
        Task<ResultResponseModel> CriarUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();
        Task<ResultResponseModel<Usuario>> LogarCadastraNumeros(LoginListaNumerosBody login);
        Task<ResultResponseModel<Usuario>> CadastrarUsuarioCadastrarNumeros(CadastrarUsuarioListaNumerosBody body);
        Task<Usuario> InformacoesDoUsuarioGanhadorPorId(int idUsuario);
        Task<IEnumerable<NumeroEscolhidoBody>> MostrarNumerosDoUsuario(int idUsuario);
        Task<IEnumerable<NumeroEscolhidoBody>> ConsultarNumerosCliente(string celularUsuario, int idSorteio);
    }
}
