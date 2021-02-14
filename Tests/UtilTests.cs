using GateSim;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class UtilTests
	{

		[TestCase(new bool[] { false, false, false, false }, 0)]
		[TestCase(new bool[] { true,  false, false, false }, 1)]
		[TestCase(new bool[] { false, true,  false, false }, 2)]
		[TestCase(new bool[] { true,  false,  true, false }, 5)]
		[TestCase(new bool[] { true,  true,  true,  true  }, 15)]
		[TestCase(new bool[] { }, 0)]
		[TestCase(new bool[] { false}, 0)]
		[TestCase(new bool[] { true }, 1)]
		public void ConvertBoolArrayToIntTests(bool[] array, int answer)
		{
			Assert.AreEqual(answer, Util.ConvertBoolArrayToInt(array));
		}

	}
}
