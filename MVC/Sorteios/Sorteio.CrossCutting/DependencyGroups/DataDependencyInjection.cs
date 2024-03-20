using Microsoft.Extensions.DependencyInjection;
using Sorteio.Data;
using Sorteio.Data.Repository;
using Sorteio.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.DependencyGroups
{
    public class DataDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SqlDataContext, SqlDataContext>();

            serviceCollection.AddTransient<ICategoriaSorteioRepository, CategoriaSorteioRepository>();
            serviceCollection.AddTransient<ITipoFormasDePagamentoRepository, TipoFormasDePagamentoRepository>();
            serviceCollection.AddTransient<IFormasDePagamentoRepository, FormasDePagamentoRepository>();
            serviceCollection.AddTransient<ISorteiosRepository, SorteiosRepository>();
            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
