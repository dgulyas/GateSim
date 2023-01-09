using GateSim;
using GateSim.InputOutput;

namespace Tests.InputOutput
{
	[TestClass]
	public class SwitchTests
	{
		private static bool[] falseArray = new bool[]{false};
		private static bool[] trueArray = new bool[]{true};

		[TestMethod]
		public void SwitchBehavesCorrectly(){
			var switch1 = new Switch(false);
			CollectionAssert.AreEqual(falseArray, switch1.Output);

			switch1.Toggle();
			CollectionAssert.AreEqual(trueArray, switch1.Output);

			switch1.Toggle();
			CollectionAssert.AreEqual(falseArray, switch1.Output);

			switch1.Set(true);
			CollectionAssert.AreEqual(trueArray, switch1.Output);
		}

	}
}
