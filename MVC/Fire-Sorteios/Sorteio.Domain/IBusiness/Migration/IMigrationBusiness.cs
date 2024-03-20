using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.IBusiness.Migration
{
    public interface IMigrationBusiness
    {
        bool ExecutarAtualizacaoBancoDados();
    }
}
