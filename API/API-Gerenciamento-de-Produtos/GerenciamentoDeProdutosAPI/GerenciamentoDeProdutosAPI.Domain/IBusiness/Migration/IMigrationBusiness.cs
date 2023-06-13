using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeProdutosAPI.Domain.IBusiness.Migration
{
    public interface IMigrationBusiness
    {
        bool ExecutarAtualizacaoBancoDados();
    }
}
