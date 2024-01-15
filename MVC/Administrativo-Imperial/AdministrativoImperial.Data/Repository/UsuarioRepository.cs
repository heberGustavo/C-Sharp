using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<UsuarioDTO, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<IList<UsuarioDTO>> Listar()
        {
            var resultData = await _dataContext.Connection.QueryAsync<UsuarioDTO>(@"SELECT
	                                                                                  UsaId
	                                                                                , UsaNome
	                                                                                , UsaEmail
                                                                                FROM
	                                                                                TB_USUARIO
                                                                                ORDER BY 
	                                                                                UsaId
                                                                                ");
            return resultData.ToList();
        }

        public async Task<UsuarioDTO> ObterUsuarioPorEmail(string email)
            => await _dataContext.Connection.QueryFirstOrDefaultAsync<UsuarioDTO>(@"SELECT 
	                                                                                            * 
                                                                                            FROM 
	                                                                                            TB_USUARIO
                                                                                            WHERE 
	                                                                                            UsaEmail = @usaEmail ",
                                                                                            new { usaEmail = email });
    }
}
