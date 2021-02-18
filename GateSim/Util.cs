using System;
using System.Linq;

namespace GateSim
{
	public static class Util
	{
		public static int ToInt(this bool[] array)
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

		public static bool[] ToBoolArray(this int num, int bitWidth)
		{
			return Convert.ToString(num, 2).PadLeft(bitWidth).Reverse().Select(s => s.Equals('1')).ToArray();
		}

		public static string ArrayToString(bool[] array)
		{
			var state = "";
			foreach (var x in array.Reverse())
			{
				state += x ? "1" : "0";
			}

			return state;
		}

		public static void SetArrayToValue(bool[] array, bool value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		public static void SetArrayToValues(bool[] array, bool[] values)
		{
			if (array.Length != values.Length)
			{
				throw new Exception("Arrays have different lengths");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = values[i];
			}
		}

	}
}
