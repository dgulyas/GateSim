using GateSim;
using GateSim.Memory;

namespace Tests.Memory
{
	[TestClass]
	public class CounterTests
	{
		[TestMethod]
		public void IncrementsOnRisingClockEdge()
		{
			var counter = new Counter(4);

			for (int i = 0; i < 15; i++)
			{
				counter.Clock[0] = false;
				counter.Tick();
				Assert.AreEqual(i, counter.Output.ToInt());
				counter.Clock[0] = true;
				counter.Tick();
				Assert.AreEqual(i+1, counter.Output.ToInt());
			}
		}

		[TestMethod]
		public void ClearsWhenClearHigh()
		{
			var counter = new Counter(4);

			for (int i = 0; i < 4; i++)
			{
				counter.Clock[0] = false;
				counter.Tick();
				counter.Clock[0] = true;
				counter.Tick();
			}
			Assert.AreEqual(4, counter.Output.ToInt());

			counter.Clear[0] = true;
			counter.Tick();
			Assert.AreEqual(0, counter.Output.ToInt());
		}

		[TestMethod]
		public void LoadsWhenLoadHigh()
		{
			var bitWidth = 4;
			var counter = new Counter(bitWidth);

			for (int i = 0; i < 4; i++)
			{
				counter.Clock[0] = false;
				counter.Tick();
				counter.Clock[0] = true;
				counter.Tick();
			}
			Assert.AreEqual(4, counter.Output.ToInt());

			counter.Clock[0] = false;
			counter.Tick();

			Util.SetArrayToValues(counter.Input, 10.ToBoolArray(bitWidth));
			counter.Load[0] = true;

			counter.Clock[0] = true;
			counter.Tick();
			Assert.AreEqual(10, counter.Output.ToInt());
		}
	}
}
