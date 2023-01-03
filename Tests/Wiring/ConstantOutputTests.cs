using GateSim;
using GateSim.Wiring;

namespace Tests.Wiring
{
	[TestClass]
	public class ConstantOutputTests
	{
		[TestMethod]
		public void ConstructorCreatesNewArray()
		{
			var newOutput = new[] { true, false, true, false };
			var output = new ConstantOutput(newOutput);
			Util.SetArrayToValues(output.Output, newOutput);
			Assert.AreEqual(true, output.Output[0]);
			Assert.AreEqual(false, output.Output[1]);
			Assert.AreEqual(true, output.Output[2]);
			Assert.AreEqual(false, output.Output[3]);
			newOutput[0] = false;
			Assert.AreEqual(true, output.Output[0]);
		}

		[TestMethod]
		public void GetStateStringWorks()
		{
			var newOutput = new[] { true, false, true, false };
			var output = new ConstantOutput(newOutput);
			var stateString = output.GetStateString();
			Assert.AreEqual("5:0101", stateString);
		}


	}
}
