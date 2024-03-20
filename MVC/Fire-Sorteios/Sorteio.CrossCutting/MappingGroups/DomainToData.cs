using AutoMapper;
using Sorteio.Data.EntityData;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<CategoriaSorteio, CategoriaSorteioData>();
            CreateMap<TipoFormaDePagamento, TipoFormaDePagamentoData>();
            CreateMap<FormasDePagamento, FormasDePagamentoData>();
            CreateMap<Sorteio.Domain.Models.EntityDomain.Sorteio, Sorteio.Data.EntityData.SorteioData>();
            CreateMap<GaleriaFotos, GaleriaFotosData>();
            CreateMap<Usuario, UsuarioData>();
            CreateMap<VencedorSorteio, VencedorSorteioData>();
        }
    }
}
