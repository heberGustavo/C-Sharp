using AdministrativoImperial.Data.EntityData;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AutoMapper;

namespace AdministrativoImperial.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<Funcionario, FuncionarioDTO>();
            CreateMap<FuncaoFuncionario, FuncaoFuncionarioDTO>();
            CreateMap<Obra, ObraDTO>();
            CreateMap<Material, MaterialDTO>();
            CreateMap<DiaTrabalhado, DiaTrabalhadoDTO>();
            CreateMap<DiaTrabalhadoFuncionario, DiaTrabalhadoFuncionarioDTO>();
        }
    }
}
