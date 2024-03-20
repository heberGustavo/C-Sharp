using Microsoft.Extensions.DependencyInjection;
using Sorteio.Domain.Business;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IBusiness.Migration;
using Sorteio.Migration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.CrossCutting.DependencyGroups
{
    public class DomainDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMigrationBusiness, MigrationBusiness>();

            serviceCollection.AddTransient<ICategoriaSorteioBusiness, CategoriaSorteioBusiness>();
            serviceCollection.AddTransient<ITipoFormasDePagamentoBusiness, TipoFormasDePagamnetoBusiness>();
            serviceCollection.AddTransient<IFormasDePagamentoBusiness, FormasDePagamentoBusiness>();
            serviceCollection.AddTransient<ISorteiosBusiness, SorteiosBusiness>();
            serviceCollection.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
        }
    }
}
