using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.Data.Repository
{
    public class FuncaoFuncionarioRepository : RepositoryBase<FuncaoFuncionarioDTO, FuncaoFuncionario>, IFuncaoFuncionarioRepository
    {
        public FuncaoFuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
