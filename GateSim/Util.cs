using System;

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

		public static string ArrayToString(bool[] array)
		{
			var state = "";
			foreach (var x in array)
			{
				state += x ? "1" : "0";
			}

			return state;
		}

	}
}
