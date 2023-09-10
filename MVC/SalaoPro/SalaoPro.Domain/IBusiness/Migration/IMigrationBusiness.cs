using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.IBusiness.Migration
{
    public interface IMigrationBusiness
    {
        bool ExecutarAtualizacaoBancoDados();
    }
}
