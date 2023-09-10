using SalaoPro.Domain.Business;
using SalaoPro.Domain.IBusiness;
using SalaoPro.Domain.IBusiness.Migration;
using SalaoPro.Migration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaoPro.CrossCutting.DependencyGroups
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
