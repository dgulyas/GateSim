using GateSim;
using GateSim.Plexers;
using NUnit.Framework;

namespace Tests.Plexers
{
	[TestFixture]
	public class MultiplexerTests
	{
		private bool[][] m_inputs;

		[OneTimeSetUp]
		public void Setup()
		{
			m_inputs = new bool[4][];
			m_inputs[0] = new[] { false, true, false, false };
			m_inputs[1] = new[] { false, true, true, false };
			m_inputs[2] = new[] { true, false, false, true };
			m_inputs[3] = new[] { true, false, false, false };
		}

		[TestCase(0, new[] {false, false})]
		[TestCase(1, new[] {true, false})]
		[TestCase(2, new[] {false, true})]
		[TestCase(3, new[] {true, true})]
		public void CorrectInputGoesToOutput(int inputNumber, bool[] selectInput)
		{
			var mux = new Multiplexer(4, 2);
			SetMuxInputs(mux);
			Util.SetArrayToValues(mux.InputSelect, selectInput);
			Assert.IsTrue(mux.Tick());
			Assert.AreEqual(m_inputs[inputNumber], mux.Output);
		}

		private void SetMuxInputs(Multiplexer m)
		{
			for (int i = 0; i < 4; i++)
			{
				Util.SetArrayToValues(m.GetInput(i), m_inputs[i]);
			}
		}

	}
}
