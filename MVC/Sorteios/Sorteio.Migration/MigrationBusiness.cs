﻿using DbUp;
using Sorteio.Common;
using Sorteio.Domain.IBusiness.Migration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Sorteio.Migration
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
