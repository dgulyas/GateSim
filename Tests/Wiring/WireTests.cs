using System.Collections.Generic;
using GateSim;
using NUnit.Framework;

namespace Tests.Wiring
{
	[TestFixture]
	public class WireTests
	{
		[Test]
		public void InputIsCopiedMultipleOutputs([Values(2,10)]int numOutputs)
		{
			var input = new [] {true, false, true, false};
			var outputs = new List<bool[]>();
			var wire = new Wire(input);

			for (int i = 0; i < numOutputs; i++)
			{
				outputs.Add(new bool[4]);
				wire.AddOutput(outputs[i]);
			}

			wire.CopyInputToOutput();

			foreach (var output in outputs)
			{
				Assert.AreEqual(input, output);
			}
		}

		[Test]
		public void InputIsCopiedToSingleOutput()
		{
			var input = new[] { true, false, true, false };
			var output = new bool[4];
			var wire = new Wire(input, output);

			Assert.AreNotEqual(input, output);
			wire.CopyInputToOutput();
			Assert.AreEqual(input, output);
		}
	}
}
