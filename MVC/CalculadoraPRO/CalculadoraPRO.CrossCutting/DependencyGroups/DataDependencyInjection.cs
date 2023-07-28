using CalculadoraPRO.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraPRO.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();
            
            //serviceCollection.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
        }
    }
}
