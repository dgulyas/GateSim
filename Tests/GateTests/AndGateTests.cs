using GateSim.Gates;
using NUnit.Framework;

namespace Tests.GateTests
{
	[TestFixture]
	public class AndGateTests
	{
		[Test]
		public void AllInputsSame([Values(1,2,20)]int numInputs, [Values(true, false)]bool inputValue)
		{
			var andGate = new AndGate(4);
			//Make sure the tick changes the output
			andGate.GetOutput()[0] = false;
			andGate.GetOutput()[1] = true;

			for (int i = 0; i < numInputs; i++)
			{
				TestHelpers.SetArrayToValue(andGate.GetNewInput(), inputValue);
			}

			Assert.IsTrue(andGate.Tick());
			Assert.AreEqual(new []{ inputValue, inputValue, inputValue, inputValue }, andGate.Output);
		}

		[Test]
		public void OneInputDifferent([Values(1, 20)]int numInputs, [Values(true, false)]bool inputValue)
		{
			var andGate = new AndGate(4);
			//Make sure the tick changes the output
			andGate.GetOutput()[0] = false;
			andGate.GetOutput()[1] = true;

			for (int i = 0; i < numInputs; i++)
			{
				TestHelpers.SetArrayToValue(andGate.GetNewInput(), inputValue);
			}
			TestHelpers.SetArrayToValue(andGate.GetNewInput(), !inputValue);

			Assert.IsTrue(andGate.Tick());
			Assert.AreEqual(new[] { false, false, false, false }, andGate.Output);
		}
	}
}
