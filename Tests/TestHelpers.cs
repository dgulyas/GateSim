using System;

namespace Tests
{
	public static class TestHelpers
	{
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
