using AutoMapper;
using BestProjects.Data.EntityData;
using BestProjects.Data.Repository.Base;
using BestProjects.Domain.IRepository;
using BestProjects.Domain.Models.EntityDomain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestProjects.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario, UsuarioData>, IUsuarioRepository
    {
        public UsuarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<int> CadastrarContato(Usuario usuario)
            => await _dataContext.Connection.ExecuteAsync(@"INSERT INTO Usuario 
                                                            (nome, telefone, celular) 
                                                            VALUES 
                                                            (@nome, @telefone, @celular);",
                                                            new { nome = usuario.nome, telefone = usuario.telefone, celular = usuario.celular });

        public async Task<int> ExcluirContato(int idUsuario)
            => await _dataContext.Connection.ExecuteAsync(@"DELETE 
                                                            FROM Usuario
                                                            WHERE id_usuario = @idUsuario;",
                                                            new { idUsuario });

        public async Task<IEnumerable<Usuario>> ObterTodosContatos()
            => await _dataContext.Connection.QueryAsync<Usuario>(@"SELECT * 
                                                             FROM Usuario u
                                                             ORDER BY u.nome");
    }
}
