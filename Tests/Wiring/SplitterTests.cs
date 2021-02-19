using System;
using System.Collections.Generic;
using System.Linq;
using GateSim;
using GateSim.Wiring;
using NUnit.Framework;

namespace Tests.Wiring
{
	[TestFixture]
	public class SplitterTests
	{
		[Test]
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

			Assert.AreEqual(out0, split.GetOutput(0));
			Assert.AreEqual(out1, split.GetOutput(1));
			Assert.AreEqual(out2, split.GetOutput(2));
			Assert.AreEqual(out3, split.GetOutput(3));
			Assert.AreEqual(out4, split.GetOutput(4));
		}

		[Test]
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
