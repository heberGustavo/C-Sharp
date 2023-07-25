using BestProjects.Domain.IBusiness.Base;
using BestProjects.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestProjects.Domain.IBusiness
{
    public interface IUsuarioBusiness : IBusinessBase<Usuario>
    {
        Task<ResultResponseModel> CadastrarContato(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterTodosContatos();
        Task<ResultResponseModel> ExcluirContato(int idContato);
    }
}
