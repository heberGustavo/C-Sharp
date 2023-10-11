using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.Data.Repository
{
    public class MaterialRepository : RepositoryBase<MaterialDTO, Material>, IMaterialRepository
    {
        public MaterialRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
