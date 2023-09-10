using SalaoPro.Data;
using SalaoPro.Data.Repository;
using SalaoPro.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaoPro.CrossCutting.DependencyGroups
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
