using AutoMapper;
using GerenciamentoDeProdutosAPI.Data.EntityData;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.CrossCutting.MappingGroups
{
    public class DataToDomain : Profile
    {
        public DataToDomain()
        {
            CreateMap<CategoriaData, CategoriaDTO>();
            CreateMap<ProdutoData, ProdutoDTO>();
        }
    }
}
