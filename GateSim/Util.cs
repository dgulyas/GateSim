using System;
using System.Linq;
using System.Text.Json;

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

		public static void Invert(this bool[] array){
			for(int i = 0; i < array.Length; i++){
				array[i] = !array[i];
			}
		}

		public static void AddOne(this bool[] a){
			for(int i = 0; i < a.Length; i++){
				if(a[i] == false){
					a[i] = true;
					break;
				}
				else{
					a[i] = false;
				}
			}
		}

		public static void MinusOne(this bool[] a){
			for(int i = 0; i < a.Length; i++){
				if(a[i] == false){
					a[i] = true;
				}
				else{
					a[i] = false;
					break;
				}
			}
		}

		//Convert an interger into a bool[] of a specific width
		public static bool[] ToBoolArray(this int num, int bitWidth)
		{
			return Convert.ToString(num, 2).PadLeft(bitWidth).Reverse().Select(s => s.Equals('1')).ToArray();
		}

		/// <summary>
		/// Converts a bool array into a string where 1 = true and 0 = false.
		/// Puts the [0]'th element on the right, so it "reverses" the string.
		/// This lets you read the bool array as a binary number with [0] as the
		/// least significant digit.
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static string ArrayToString(this bool[] array)
		{
			var state = "";
			foreach (var x in array.Reverse())
			{
				state += x ? "1" : "0";
			}

			return state;
		}

		public static bool[] ToBoolArray(this string data)
		{
			var array = new bool[data.Length];
			for(int i = 0; i < array.Length; i++)
			{
				var dataIndex = data.Length - i - 1;
				if (data[dataIndex] == '1')
				{
					array[i] = true;
				}
				else if (data[dataIndex] == '0')
				{
					array[i] = false;
				}
				else
				{
					throw new Exception("String must only have '0' or '1' in it");
				}
			}
			return array;
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

		public static void WriteArrayToFile(string filePath, int[] array){
			var jsonString = JsonSerializer.Serialize(array);
			File.WriteAllText(filePath, jsonString);
		}

		public static int[] ReadArrayFromFile(string filePath){
			var jsonString = File.ReadAllText(filePath);
			var array = JsonSerializer.Deserialize<int[]>(jsonString);
			if(array == null){
				throw new Exception($"Problem deserializing int array in {filePath}");
			}
			return array;
		}

	}
}
