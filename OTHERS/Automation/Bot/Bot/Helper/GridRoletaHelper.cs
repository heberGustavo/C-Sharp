using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Helper
{
	public class GridRoletaHelper
	{
		#region Duzias

		public static readonly List<int> Duzia1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
		public static readonly List<int> Duzia2 = new List<int> { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
		public static readonly List<int> Duzia3 = new List<int> { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };

		#endregion

		#region Colunas

		public static readonly List<int> Coluna1 = new List<int> { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
		public static readonly List<int> Coluna2 = new List<int> { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
		public static readonly List<int> Coluna3 = new List<int> { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };

        #endregion
    }
}
