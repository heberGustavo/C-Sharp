using Bot.Helper;
using Bot.Notification.Telegram;
using Bot.Notification.Telegram.Helper;
using Bot.Strategy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V120.Debugger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
	public class AutomationWeb
	{
		public IWebDriver driver;
		public Dictionary<string, int> valuesStatisticsDuzias = new Dictionary<string, int>();
		public Dictionary<string, int> valuesStatisticsColunas = new Dictionary<string, int>();

        public AutomationWeb() {}

        public void Initialize()
        {
			try
			{
				driver = new ChromeDriver();
				driver.Navigate().GoToUrl("https://livecasino.bet365.com/Play/SuperSpinRoulette");

				Console.WriteLine("ETAPA 1: Redirecionando para pagina");

				Console.WriteLine("ETAPA 2: Aguarde 3s para carregar todos os componentes");
				Task.Delay(3000).Wait();

				LoginPlataform();
				Console.WriteLine("ETAPA 3: Login na Plataforma");

				Console.WriteLine("ETAPA 4: Aguarde 25s para carregar a live");
				Task.Delay(25000).Wait();

				Console.WriteLine("ETAPA 5: Abrir estatisticas");
				OpenStatistics();

				Console.WriteLine("ETAPA 5: Inicio da verificação dos Históricos");
				GetHistory();
			}
			catch (Exception ex)
			{
				CloseAndOpenApplicationAgain();
			}
            
		}

		#region Métodos Privados

		private void LoginPlataform()
		{
			IWebElement inputUser = driver.FindElement(By.Id("txtUsername"));
			IWebElement inputPassword = driver.FindElement(By.Id("txtPassword"));
			IWebElement btnLoginModal = driver.FindElement(By.ClassName("login-modal-component__login-button-text"));

			inputUser.SendKeys("heber_gustavoo");
			inputPassword.SendKeys("gustavo10,9");
			btnLoginModal.Click();
		}

		private void OpenStatistics()
		{
			VerifyFrames();

			var btnStatistics = driver.FindElements(By.ClassName("sidebar-buttons__item"));
			if (btnStatistics != null)
				btnStatistics[3].Click();

			var btn100 = driver.FindElements(By.ClassName("tabs__option"));
			if (btn100 != null)
				btn100[0].Click();
		}

		private void GetHistory()
		{
			ReadOnlyCollection<IWebElement>? historyValuesHelper = null;
			var date = DateTime.Now.AddMinutes(5);

			while (true)
			{
				VerifyReset(date);
				historyValuesHelper = LoadingHistoryValues(historyValuesHelper);
			}
		}

		#endregion

		#region Métodos Internos

		private void VerifyFrames()
		{
			var iframes = driver.FindElements(By.TagName("iframe"));
			if (iframes.Count <= 0)
				Console.WriteLine("Erro: Frame não encontrado!");

			for (int i = 0; i < iframes.Count; i++)
			{
				driver.SwitchTo().Frame(iframes[i]);

				var iframesItem = driver.FindElements(By.TagName("iframe"));
				if (iframesItem.Count > 0)
				{
					driver.SwitchTo().Frame(iframesItem[0]);
					return;
				}
				else
					driver.SwitchTo().DefaultContent();
			}
		}

		private ReadOnlyCollection<IWebElement> LoadingHistoryValues(ReadOnlyCollection<IWebElement>? historyValuesHelper)
		{
			var historyValues = driver.FindElements(By.ClassName("roulette-history-item__value-text--siwxW"));
			
			if (historyValuesHelper == null)
			{
				historyValuesHelper = historyValues;
				PrintHistory(historyValuesHelper);
			}
			else if (!historyValuesHelper.SequenceEqual(historyValues))
			{
				historyValuesHelper = historyValues;

				GetStatistics();
				PrintHistory(historyValuesHelper);

				var strategyDuzia_1 = new StrategyDuzia_1();
				strategyDuzia_1.Strategy(valuesStatisticsDuzias, historyValuesHelper);
			}


			return historyValuesHelper;
		}

		private void GetStatistics()
		{
			valuesStatisticsDuzias = new Dictionary<string, int>();
			valuesStatisticsColunas = new Dictionary<string, int>();

			Task.Delay(800).Wait();

			var element = driver.FindElements(By.ClassName("roulette-statistics-info__position-title-text--ykAV0"));

			if(element != null){
				#region Duzias

				valuesStatisticsDuzias.Add(DictionaryHelper.Duzia1, Convert.ToInt32(element[7].Text.Split('%')[0]));
				valuesStatisticsDuzias.Add(DictionaryHelper.Duzia2, Convert.ToInt32(element[8].Text.Split('%')[0]));
				valuesStatisticsDuzias.Add(DictionaryHelper.Duzia3, Convert.ToInt32(element[9].Text.Split('%')[0]));

				#endregion

				#region Colunas

				valuesStatisticsColunas.Add(DictionaryHelper.Coluna1, Convert.ToInt32(element[10].Text.Split('%')[0]));
				valuesStatisticsColunas.Add(DictionaryHelper.Coluna2, Convert.ToInt32(element[11].Text.Split('%')[0]));
				valuesStatisticsColunas.Add(DictionaryHelper.Coluna3, Convert.ToInt32(element[12].Text.Split('%')[0]));

				#endregion
			}

			return;
		}

		private void PrintHistory(ReadOnlyCollection<IWebElement>? historyValuesHelper)
		{
			if (historyValuesHelper != null)
			{
				Console.WriteLine("");
				Console.WriteLine("Historico");

				foreach (var item in historyValuesHelper)
				{
					Console.WriteLine(item.Text);
				}

				Console.WriteLine("=====END=====");
			}
		}

		private void VerifyReset(DateTime date)
		{
			if (DateTime.Now >= date)
			{
				CloseAndOpenApplicationAgain();
				return;
			}
		}

		private void CloseAndOpenApplicationAgain()
		{
			driver.Close();
			driver.Quit();

			Initialize();
		}

		#endregion

	}
}
