using GateSim.Simulations;

namespace Tests.Simulations
{
	[TestClass]
	public class AdderTests
	{
		[DataTestMethod]
		[DataRow(false, false, false, false, false)]
		[DataRow(false, false, true, true, false)]
		[DataRow(false, true, false, true, false)]
		[DataRow(false, true, true, false, true)]
		[DataRow(true, false, false, true, false)]
		[DataRow(true, false, true, false, true)]
		[DataRow(true, true, false, false, true)]
		[DataRow(true, true, true, true, true)]
		public void AdderTest(bool aInput, bool bInput, bool cInput, bool sOutputExpected, bool cOutputExpected)
		{
			var adder = new AdderSim();
			var (sOutput, cOutput) = adder.Run(aInput, bInput, cInput);
			Assert.AreEqual(sOutputExpected, sOutput);
			Assert.AreEqual(cOutputExpected, cOutput);
		}

	}
}
