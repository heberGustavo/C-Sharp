using Bot.Helper;
using Bot.Notification.Telegram;
using Bot.Notification.Telegram.Helper;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Strategy
{
	/*
	 *	1- Verificar se a MENOR PORCENTAGEM é inferior a 29% e se a MAIOR esta com menos de 40%, se sim continuar
		2- Verificar se o historico saiu 4 ou menos numeros do grafico com menor porcentagem, se menor continuar
	 */
	public class StrategyDuzia_1
	{
		private const int MIN_VALUE_VALIDATION = 28;
		private const int MAX_VALUE_VALIDATION = 40;
        public StrategyDuzia_1() {}

		public void Strategy(Dictionary<string, int> valuesStatisticsDuzias, ReadOnlyCollection<IWebElement> valuesHistory)
		{
			var minValue = valuesStatisticsDuzias.OrderBy(x => x.Value).First();
			var maxValue = valuesStatisticsDuzias.OrderByDescending(x => x.Value).First();

			if(minValue.Value <= MIN_VALUE_VALIDATION && maxValue.Value <= MAX_VALUE_VALIDATION)
			{
				var listValidationMin = ReturnListValidation(minValue);
				if(listValidationMin != null)
				{
					int quantMinInHistory = valuesHistory.Count(item => listValidationMin.Contains(Convert.ToInt32(item.Text)));
					var quantSameDuzia2FirstItems = valuesHistory.Take(2).Count(item => listValidationMin.Contains(Convert.ToInt32(item.Text)));
					Console.WriteLine(quantSameDuzia2FirstItems);

					if(quantMinInHistory <= 4) {

						var message =
							$"🛑 ATENÇÃO! 🛑\n" +
							$"Possivel entrada encontrada! \n\n" +
							$"Data: {DateTime.Now} \n" +
							$"Tipo entrada: DUZIA \n\n" +
							$"Entrada: CONTRA A '{minValue.Key.ToUpper()}' \n" +
							$"Quant. no Histórico: {quantMinInHistory} \n";

						TelegramNotification.TelegramSendMessage(DictionaryTelegramHelper.TokenAPI, DictionaryTelegramHelper.ChatId, message);
					}
				}
			}
		}

		public List<int> ReturnListValidation(KeyValuePair<string, int> value)
		{
			var list = new List<int>();

			if (value.Key.Equals(DictionaryHelper.Duzia1))
				list = GridRoletaHelper.Duzia1;
			else if (value.Key.Equals(DictionaryHelper.Duzia2))
				list = GridRoletaHelper.Duzia2;
			else if (value.Key.Equals(DictionaryHelper.Duzia3))
				list = GridRoletaHelper.Duzia3;

			else if (value.Key.Equals(DictionaryHelper.Coluna1))
				list = GridRoletaHelper.Coluna1;
			else if (value.Key.Equals(DictionaryHelper.Coluna2))
				list = GridRoletaHelper.Coluna2;
			else if (value.Key.Equals(DictionaryHelper.Coluna3))
				list = GridRoletaHelper.Coluna3;

			return list;
		}
	}
	
}
