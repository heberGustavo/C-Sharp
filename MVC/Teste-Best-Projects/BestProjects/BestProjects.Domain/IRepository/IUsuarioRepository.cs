using BestProjects.Domain.IRepository.Base;
using BestProjects.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestProjects.Domain.IRepository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<int> CadastrarContato(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterTodosContatos();
        Task<int> ExcluirContato(int idContato);
    }
}
