using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Data.EntityData;
using Teste.Domain.Models.EntityDomain;

namespace Teste.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<Funcionario, FuncionarioData>();
            CreateMap<Filho, FilhoData>();
        }
    }
}
