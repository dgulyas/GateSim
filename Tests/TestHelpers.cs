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
	}
}
