using AdministrativoImperial.Domain.Business;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IBusiness.Migration;
using AdministrativoImperial.Migration;
using Microsoft.Extensions.DependencyInjection;

namespace AdministrativoImperial.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();

            serviceCollection.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
            serviceCollection.AddTransient<IFuncionarioBusiness, FuncionarioBusiness>();
            serviceCollection.AddTransient<IFuncaoFuncionarioBusiness, FuncaoFuncionarioBusiness>();
            serviceCollection.AddTransient<IObraBusiness, ObraBusiness>();
            serviceCollection.AddTransient<IMaterialBusiness, MaterialBusiness>();
            serviceCollection.AddTransient<IDiaTrabalhadoBusiness, DiaTrabalhadoBusiness>();  
        }
    }
}
