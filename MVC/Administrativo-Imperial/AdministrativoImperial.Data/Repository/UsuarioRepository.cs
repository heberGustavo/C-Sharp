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

        public async Task<IList<ObraDTO>> Listar()
        {
            var resultData = await _dataContext.Connection.QueryAsync<ObraDTO>(@"SELECT
	                                                                              ObrId
	                                                                            , ObrApelido
	                                                                            , ObrDataInicio
	                                                                            , ObrDataFim
	                                                                            , ObrEndereco
	                                                                            , ObrOrcamento
	                                                                            , ObrStatus
                                                                            FROM
	                                                                            TB_OBRA
                                                                            ORDER BY 
	                                                                            ObrStatus, ObrApelido
                                                                            ");
            return resultData.ToList();
        }

        public Task<UsuarioDTO> ObterUsuario(string email, string senha)
            => _dataContext.Connection.QueryFirstOrDefault(@"SELECT 
	                                                            * 
                                                            FROM 
	                                                            TB_USUARIO
                                                            WHERE 
	                                                            UsaEmail = @usaEmail 
	                                                            AND UsaSenha = @usaSenha",
                                                            new { usaEmail = email, usaSenha = senha });
    }
}
