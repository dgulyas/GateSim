using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateSim
{
	public static class Util
	{
		public static int ConvertBoolArrayToInt(bool[] array)
		{
			var workingTotal = 0;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i])
				{
					workingTotal += (int)Math.Pow(2, i);
				}
			}

			return workingTotal;
		}

	}
}
