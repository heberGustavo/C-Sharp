using System;
using System.Collections.Generic;
using System.Text;

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
