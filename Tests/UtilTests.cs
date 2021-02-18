using GateSim;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class UtilTests
	{
		[TestCase(new [] { false, false, false, false }, 0)]
		[TestCase(new [] { true,  false, false, false }, 1)]
		[TestCase(new [] { false, true,  false, false }, 2)]
		[TestCase(new [] { true,  false,  true, false }, 5)]
		[TestCase(new [] { true,  true,  true,  true  }, 15)]
		[TestCase(new bool[] { }, 0)]
		[TestCase(new [] { false}, 0)]
		[TestCase(new [] { true }, 1)]
		public void ConvertBoolArrayToIntTests(bool[] array, int answer)
		{
			Assert.AreEqual(answer, array.ToInt());
		}

		[TestCase(0, new [] { false, false, false, false })]
		[TestCase(1, new [] { true, false, false, false })]
		[TestCase(2, new [] { false, true, false, false })]
		[TestCase(5, new [] { true, false, true, false })]
		[TestCase(15, new [] { true, true, true, true })]
		public void ToBoolArrayTests(int input, bool[] answer)
		{
			Assert.AreEqual(answer, input.ToBoolArray(4));
		}

		[TestCase(new [] {true, false, false}, "001")]
		[TestCase(new [] {false, false, false}, "000")]
		[TestCase(new [] {true, true, false}, "011")]
		[TestCase(new [] {true}, "1")]
		[TestCase(new bool[] {}, "")]
		public void ArrayToStringTests(bool[] array, string expectedValue)
		{
			Assert.AreEqual(expectedValue, Util.ArrayToString(array));
		}

	}
}
