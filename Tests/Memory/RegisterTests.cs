using GateSim;
using GateSim.Memory;
using NUnit.Framework;

namespace Tests.Memory
{
	[TestFixture]
	public class RegisterTests
	{
		private int m_bitWidth = 4;
		private readonly bool[] m_variedInput = { true, false, true, false };
		private readonly bool[] m_allFalseInput = { false, false, false, false };

	[Test]
		public void RisingEdgeSavesInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(m_allFalseInput, reg.Output);
			Assert.AreEqual(false, reg.Clock[0]);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = true;

			Assert.IsTrue(reg.Tick());
			Assert.AreEqual(newInput, reg.Output);
		}

		[Test]
		public void ConstantLowIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			Assert.AreEqual(m_allFalseInput, reg.Output);
			Assert.AreEqual(false, reg.Clock[0]);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(m_allFalseInput, reg.Output);
		}

		[Test]
		public void FallingEdgeIgnoresInput()
		{
			var reg = new Register(m_bitWidth);
			reg.Clock[0] = true;
			reg.Tick();
			Assert.AreEqual(m_allFalseInput, reg.Output);
			var newInput = m_variedInput;
			Util.SetArrayToValues(reg.Input, newInput);
			reg.Clock[0] = false;

			Assert.IsFalse(reg.Tick());
			Assert.AreEqual(m_allFalseInput, reg.Output);
		}

	}
}
