using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<FuncionarioData, FuncionarioDTO>();
            CreateMap<FuncaoFuncionarioData, FuncaoFuncionarioDTO>();
            CreateMap<ObraData, ObraDTO>();
        }
    }
}
