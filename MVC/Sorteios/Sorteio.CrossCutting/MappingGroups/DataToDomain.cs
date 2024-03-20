using AutoMapper;
using Sorteio.Data.EntityData;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<CategoriaSorteioData, CategoriaSorteio>();
            CreateMap<TipoFormaDePagamentoData, TipoFormaDePagamento>();
            CreateMap<FormasDePagamentoData, FormasDePagamento>();
            CreateMap<Sorteio.Data.EntityData.SorteioData, Sorteio.Domain.Models.EntityDomain.Sorteio>();
            CreateMap<GaleriaFotosData, GaleriaFotos>();
            CreateMap<UsuarioData, Usuario>();
            CreateMap<VencedorSorteioData, VencedorSorteio>();
        }
    }
}
