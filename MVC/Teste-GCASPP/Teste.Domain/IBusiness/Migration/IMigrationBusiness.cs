using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.IBusiness.Migration
{
    public interface IMigrationBusiness
    {
        bool ExecutarAtualizacaoBancoDados();
    }
}
