using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.Data.Repository
{
    public class FuncaoFuncionarioRepository : RepositoryBase<FuncaoFuncionario, FuncaoFuncionarioData>, IFuncaoFuncionarioRepository
    {
        public FuncaoFuncionarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
