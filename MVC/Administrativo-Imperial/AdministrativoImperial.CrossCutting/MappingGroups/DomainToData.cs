using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<FuncionarioDTO, Funcionario>();
            CreateMap<FuncaoFuncionarioDTO, FuncaoFuncionario>();
            CreateMap<ObraDTO, Obra>();
            CreateMap<MaterialDTO, Material>();
            CreateMap<DiaTrabalhadoDTO, DiaTrabalhado>();
            CreateMap<DiaTrabalhadoFuncionarioDTO, DiaTrabalhadoFuncionario>();
        }
    }
}
