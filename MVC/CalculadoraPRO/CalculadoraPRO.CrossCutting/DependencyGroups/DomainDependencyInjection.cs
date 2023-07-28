using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraPRO.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();

            //serviceCollection.AddTransient<IFuncionarioBusiness, FuncionarioBusiness>();
        }
    }
}
