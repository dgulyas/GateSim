using GateSim;
using GateSim.Gates;

namespace Tests.GateTests
{
	[TestClass]
	public class AndGateTests
	{
		[DataTestMethod]
		[DataRow(1, true)]
		[DataRow(2, true)]
		[DataRow(20, true)]
		[DataRow(1, false)]
		[DataRow(2, false)]
		[DataRow(20, false)]
		public void AllInputsSame(int numInputs, bool inputValue)
		{
			var andGate = new AndGate(4);
			//Make sure the tick changes the output
			andGate.Output[0] = false;
			andGate.Output[1] = true;

			for (int i = 0; i < numInputs; i++)
			{
				Util.SetArrayToValue(andGate.GetNewInput(), inputValue);
			}

			Assert.IsTrue(andGate.Tick());
			CollectionAssert.AreEqual(new []{ inputValue, inputValue, inputValue, inputValue }, andGate.Output);
		}

		[DataTestMethod]
		[DataRow(1, true)]
		[DataRow(20, true)]
		[DataRow(1, false)]
		[DataRow(20, false)]
		public void OneInputDifferent(int numInputs, bool inputValue)
		{
			var andGate = new AndGate(4);
			//Make sure the tick changes the output
			andGate.Output[0] = false;
			andGate.Output[1] = true;

			for (int i = 0; i < numInputs; i++)
			{
				Util.SetArrayToValue(andGate.GetNewInput(), inputValue);
			}
			Util.SetArrayToValue(andGate.GetNewInput(), !inputValue);

			Assert.IsTrue(andGate.Tick());
			CollectionAssert.AreEqual(new[] { false, false, false, false }, andGate.Output);
		}
	}
}
