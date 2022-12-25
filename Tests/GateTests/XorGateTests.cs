using GateSim;
using GateSim.Gates;

namespace Tests.GateTests
{
	[TestClass]
	public class XorGateTests
	{
		[DataTestMethod] 
		[DataRow(1, true)]
		[DataRow(2, false)]
		[DataRow(3, true)]
		[DataRow(20, false)]
		public void AllInputsSame(int numInputs, bool expectedOutput)
		{
			var xorGate = new XorGate(4);
			//Make sure the tick changes the output
			xorGate.Output[0] = false;
			xorGate.Output[1] = true;

			for (int i = 0; i < numInputs; i++)
			{
				Util.SetArrayToValue(xorGate.GetNewInput(), true);
			}

			Assert.IsTrue(xorGate.Tick());
			CollectionAssert.AreEqual(new[] { expectedOutput, expectedOutput, expectedOutput, expectedOutput }, xorGate.Output);
		}
	}
}
