using CalculadoraPRO.Domain.IBusiness.Migration;
using CrossCutting.Migration;
using Microsoft.Extensions.DependencyInjection;

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
