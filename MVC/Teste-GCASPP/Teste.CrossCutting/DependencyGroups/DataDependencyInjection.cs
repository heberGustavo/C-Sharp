using Teste.Data;
using Teste.Data.Repository;
using Teste.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();

            serviceCollection.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            serviceCollection.AddTransient<IFilhoRepository, FilhoRepository>();
        }
    }
}
