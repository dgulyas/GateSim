using GateSim;
using GateSim.Memory;
using NUnit.Framework;

namespace Tests.Memory
{
	[TestFixture]
	public class RegisterTests
	{
		private readonly int m_bitWidth = 4;
		private readonly bool[] m_variedInput = { true, false, true, false };
		private readonly bool[] m_allFalseInput = { false, false, false, false };

		[Test]
		public void RisingClockEdgeSavesInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(m_allFalseInput, reg.Output);
			Assert.AreEqual(false, reg.Clock[0]);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = true;
			reg.Enable[0] = true;

			Assert.IsTrue(reg.Tick());
			Assert.AreEqual(newInput, reg.Output);
		}

		[Test]
		public void ConstantLowClockIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(m_allFalseInput, reg.Output);
			Assert.AreEqual(false, reg.Clock[0]);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Enable[0] = true;

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(m_allFalseInput, reg.Output);
		}

		[Test]
		public void FallingClockEdgeIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			reg.Clock[0] = true;
			reg.Enable[0] = true;
			reg.Tick();
			Assert.AreEqual(m_allFalseInput, reg.Output);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = false;

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(m_allFalseInput, reg.Output);
		}

		[Test]
		public void LowEnableIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(m_allFalseInput, reg.Output);
			Assert.AreEqual(false, reg.Clock[0]);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = true;
			reg.Enable[0] = false;

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(m_allFalseInput, reg.Output);
		}

	}
}
