using GerenciamentoDeProdutosAPI.Data;
using GerenciamentoDeProdutosAPI.Data.Repository;
using GerenciamentoDeProdutosAPI.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();

            serviceCollection.AddTransient<ICategoriaRepository, CategoriaRepository>();
            serviceCollection.AddTransient<IProdutoRepository, ProdutoRepository>();
        }
    }
}
