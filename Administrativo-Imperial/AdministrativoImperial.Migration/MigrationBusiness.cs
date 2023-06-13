using AdministrativoImperial.Common;
using AdministrativoImperial.Domain.IBusiness.Migration;
using DbUp;
using System.Diagnostics;
using System.Reflection;

namespace AdministrativoImperial.Migration
{
    public class MigrationBusiness : IMigrationBusiness
    {
        public bool ExecutarAtualizacaoBancoDados()
        {
            var connectionString = APICoreCommon.GetValueSetting("CONNECTION_STRING");

            var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .WithTransactionPerScript()
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Debug.WriteLine("Erro ao rodar script");
                Debug.WriteLine(result.Error);
                Debug.WriteLine(result.Error.InnerException);
            }

            return result.Successful;
        }
    }
}
