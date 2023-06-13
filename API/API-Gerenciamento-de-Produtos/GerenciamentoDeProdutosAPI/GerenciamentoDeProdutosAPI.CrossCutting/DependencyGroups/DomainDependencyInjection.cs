using GerenciamentoDeProdutosAPI.Domain.Business;
using GerenciamentoDeProdutosAPI.Domain.IBusiness;
using GerenciamentoDeProdutosAPI.Domain.IBusiness.Migration;
using GerenciamentoDeProdutosAPI.Migration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();

            serviceCollection.AddTransient<ICategoriaBusiness, CategoriaBusiness>();
            serviceCollection.AddTransient<IProdutoBusiness, ProdutoBusiness>();
        }
    }
}
