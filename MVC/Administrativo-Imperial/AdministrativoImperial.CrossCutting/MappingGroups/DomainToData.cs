using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<FuncionarioDTO, FuncionarioData>();
            CreateMap<FuncaoFuncionarioDTO, FuncaoFuncionarioData>();
            CreateMap<ObraDTO, ObraData>();
        }
    }
}
