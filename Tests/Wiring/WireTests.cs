﻿using System.Collections.Generic;
using GateSim.Wiring;

namespace Tests.Wiring
{
	[TestClass]
	public class WireTests
	{
		[DataTestMethod]
		[DataRow(2)]
		[DataRow(10)]
		public void InputIsCopiedMultipleOutputs(int numOutputs)
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
				CollectionAssert.AreEqual(input, output);
			}
		}

		[TestMethod]
		public void InputIsCopiedToSingleOutput()
		{
			var input = new[] { true, false, true, false };
			var output = new bool[4];
			var wire = new Wire(input, output);

			Assert.AreNotEqual(input, output);
			wire.CopyInputToOutput();
			CollectionAssert.AreEqual(input, output);
		}
	}
}
