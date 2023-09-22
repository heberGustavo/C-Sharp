using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class ObraRepository : RepositoryBase<ObraDTO, ObraData>, IObraRepository
    {
        public ObraRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

    }
}
