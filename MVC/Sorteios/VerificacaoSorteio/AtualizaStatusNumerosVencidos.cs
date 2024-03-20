using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sorteio.Domain.IBusiness;
using Sorteio.Common;

namespace VerificacaoSorteio
{
    public class AtualizaStatusNumerosVencidos
    {
        private readonly ISorteiosBusiness _sorteiosBusiness;

        public AtualizaStatusNumerosVencidos(ISorteiosBusiness sorteiosBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
        }

        [FunctionName("AtualizaStatusNumerosVencidos")]
        public async Task Run([TimerTrigger("0 0 2 * * *", RunOnStartup = true)] TimerInfo timerInfo, ILogger log)
        {
            try
            {
                var pedidos = await _sorteiosBusiness.ObterTodosPedidosPendentes();

                DateTime dataAtual = DateTime.Now;
                foreach (var item in pedidos)
                {
                    if (item.data_pedido.ToString("dd/MM/yyyy") != dataAtual.ToString("dd/MM/yyyy"))
                    {
                        DateTime dataFim = item.data_pedido.AddDays(DataDictionary.DIAS_MAXIMO_PAGAMENTO);

                        var diferencaEntreDatas = (int)dataAtual.Subtract(dataFim).TotalDays;

                        if (diferencaEntreDatas > 0)
                        {
                            await _sorteiosBusiness.RemoverPedidoPendenteAposPrazoMaximo(item);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                log.LogError("Meu erro: " + e.Message);
                log.LogError("StackTrace: " + e.StackTrace);
            }

        }
    }
}
