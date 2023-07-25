using Teste.Domain.Business;
using Teste.Domain.IBusiness;
using Teste.Domain.IBusiness.Migration;
using Teste.Migration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();

            serviceCollection.AddTransient<IFuncionarioBusiness, FuncionarioBusiness>();
            serviceCollection.AddTransient<IFilhoBusiness, FilhoBusiness>();
        }
    }
}
