using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraPRO.Domain.IBusiness.Migration
{
    public interface IMigrationBusiness
    {
        bool ExecutarAtualizacaoBancoDados();
    }
}
