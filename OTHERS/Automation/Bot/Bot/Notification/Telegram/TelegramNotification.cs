using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Notification.Telegram
{
	public static class TelegramNotification
	{
		public static string TelegramSendMessage(string apilToken, string destID, string text)
		{
			string urlString = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={destID}&text={text}";

			WebClient webclient = new WebClient();

			return webclient.DownloadString(urlString);
		}
	}
}
