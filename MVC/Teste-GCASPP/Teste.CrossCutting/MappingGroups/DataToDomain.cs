using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Data.EntityData;
using Teste.Domain.Models.EntityDomain;

namespace Teste.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<FuncionarioData, Funcionario>();
            CreateMap<FilhoData, Filho>();
        }
    }
}
