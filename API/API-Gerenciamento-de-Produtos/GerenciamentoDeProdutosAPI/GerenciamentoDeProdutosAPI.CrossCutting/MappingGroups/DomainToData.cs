using AutoMapper;
using GerenciamentoDeProdutosAPI.Data.EntityData;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.CrossCutting.MappingGroups
{
    public class DomainToData : Profile
    {
        public DomainToData()
        {
            CreateMap<CategoriaDTO, CategoriaData>();
            CreateMap<ProdutoDTO, ProdutoData>();
        }
    }
}
