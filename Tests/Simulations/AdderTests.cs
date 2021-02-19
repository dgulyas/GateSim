using GateSim.Simulations;
using NUnit.Framework;

namespace Tests.Simulations
{
	[TestFixture]
	public class AdderTests
	{
		[TestCase(false, false, false, false, false)]
		[TestCase(false, false, true, true, false)]
		[TestCase(false, true, false, true, false)]
		[TestCase(false, true, true, false, true)]
		[TestCase(true, false, false, true, false)]
		[TestCase(true, false, true, false, true)]
		[TestCase(true, true, false, false, true)]
		[TestCase(true, true, true, true, true)]
		public void AdderTest(bool aInput, bool bInput, bool cInput, bool sOutputExpected, bool cOutputExpected)
		{
			var adder = new Adder();
			var (sOutput, cOutput) = adder.Run(aInput, bInput, cInput);
			Assert.AreEqual(sOutputExpected, sOutput[0]);
			Assert.AreEqual(cOutputExpected, cOutput[0]);
		}

	}
}
