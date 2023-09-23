using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<Funcionario, FuncionarioDTO>();
            CreateMap<FuncaoFuncionario, FuncaoFuncionarioDTO>();
            CreateMap<Obra, ObraDTO>();
        }
    }
}
