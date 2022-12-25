using System;
using System.Collections.Generic;
using System.Linq;
using GateSim;
using GateSim.Wiring;

namespace Tests.Wiring
{
	[TestClass]
	public class SplitterTests
	{
		[TestMethod]
		public void InputIsCopiedToOutputsCorrectly()
		{
			var out0 = new [] {true};
			var out1 = new [] {true, false};
			var out2 = new [] {true, false, true};
			var out3 = new [] {false, true, true, false, false, true};
			var out4 = new [] {true, false, true, false};
			var input = out0.Concat(out1).Concat(out2).Concat(out3).Concat(out4).ToArray();

			var mapping = new Dictionary<int, Tuple<int, int>>
			{
				{0, new Tuple<int, int>(0, 0)},
				{1, new Tuple<int, int>(1, 2)},
				{2, new Tuple<int, int>(3, 5)},
				{3, new Tuple<int, int>(6, 11)},
				{4, new Tuple<int, int>(12, 15)}
			};

			var split = new Splitter(16, mapping);
			Util.SetArrayToValues(split.Input, input);

			Assert.IsTrue(split.Tick());

			CollectionAssert.AreEqual(out0, split.GetOutput(0));
			CollectionAssert.AreEqual(out1, split.GetOutput(1));
			CollectionAssert.AreEqual(out2, split.GetOutput(2));
			CollectionAssert.AreEqual(out3, split.GetOutput(3));
			CollectionAssert.AreEqual(out4, split.GetOutput(4));
		}

		[TestMethod]
		public void TickReturnsFalseIfNothingChanged()
		{
			var out0 = new[] { true };
			var out1 = new[] { true, false };
			var input = out0.Concat(out1).ToArray();

			var mapping = new Dictionary<int, Tuple<int, int>>
			{
				{0, new Tuple<int, int>(0, 0)},
				{1, new Tuple<int, int>(1, 2)}
			};

			var split = new Splitter(3, mapping);
			Util.SetArrayToValues(split.Input, input);

			Assert.IsTrue(split.Tick());
			Assert.IsFalse(split.Tick());
		}

	}
}
