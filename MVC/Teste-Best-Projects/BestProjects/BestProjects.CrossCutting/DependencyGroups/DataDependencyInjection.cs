using BestProjects.Data;
using BestProjects.Data.Repository;
using BestProjects.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestProjects.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();

            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
