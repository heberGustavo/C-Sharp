using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IRepository
{
    public interface IDiaTrabalhadoRepository : IRepositoryBase<DiaTrabalhadoDTO>
    {
        Task<IList<DiaTrabalhadoDTO>> Listar();
    }
}
