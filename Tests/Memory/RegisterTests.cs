using System;
using System.Collections.Generic;
using System.Linq;
using GateSim.Memory;
using NUnit.Framework;

namespace Tests.Memory
{
	[TestFixture]
	class RegisterTests
	{
		private int m_bitWidth = 4;
		private bool[] variedInput = { true, false, true, false };
		private bool[] allFalseInput = { false, false, false, false };

	[Test]
		public void RisingEdgeSavesInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(allFalseInput, reg.Output);
			var newInput = variedInput;
			TestHelpers.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = true;

			Assert.IsTrue(reg.Tick());
			Assert.AreEqual(newInput, reg.Output);
		}

		[Test]
		public void ConstantLowIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(allFalseInput, reg.Output);
			var newInput = variedInput;
			TestHelpers.SetArrayToValues(reg.Input, newInput);

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(allFalseInput, reg.Output);
		}

		[Test]
		public void FallingEdgeIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			reg.Clock[0] = true;
			reg.Tick();
			Assert.AreEqual(allFalseInput, reg.Output);
			var newInput = variedInput;
			TestHelpers.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = false;

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(allFalseInput, reg.Output);
		}

	}
}
