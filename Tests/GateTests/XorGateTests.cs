﻿using GateSim;
using GateSim.Gates;
using NUnit.Framework;

namespace Tests.GateTests
{
	[TestFixture]
	public class XorGateTests
	{
		[TestCase(1, true)]
		[TestCase(2, false)]
		[TestCase(3, true)]
		[TestCase(20, false)]
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
			Assert.AreEqual(new[] { expectedOutput, expectedOutput, expectedOutput, expectedOutput }, xorGate.Output);
		}
	}
}
