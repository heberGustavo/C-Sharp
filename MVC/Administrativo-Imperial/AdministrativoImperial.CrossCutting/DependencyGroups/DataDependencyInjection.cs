using AdministrativoImperial.Data;
using AdministrativoImperial.Data.Repository;
using AdministrativoImperial.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace AdministrativoImperial.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();

            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            serviceCollection.AddTransient<IFuncaoFuncionarioRepository, FuncaoFuncionarioRepository>();
            serviceCollection.AddTransient<IObraRepository, ObraRepository>();
            serviceCollection.AddTransient<IMaterialRepository, MaterialRepository>();
            serviceCollection.AddTransient<IDiaTrabalhadoRepository, DiaTrabalhadoRepository>(); 
            serviceCollection.AddTransient<IDiaTrabalhadoFuncionarioRepository, DiaTrabalhadoFuncionarioRepository>(); 
        }
    }
}
