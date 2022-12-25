using GateSim;
using GateSim.Plexers;

namespace Tests.Plexers
{
	[TestClass]
	public class MultiplexerTests
	{
		private bool[][] m_inputs;

		public MultiplexerTests(){
			m_inputs = new bool[4][];
			m_inputs[0] = new[] { false, true, false, false };
			m_inputs[1] = new[] { false, true, true, false };
			m_inputs[2] = new[] { true, false, false, true };
			m_inputs[3] = new[] { true, false, false, false };
		}

		[DataTestMethod]
		[DataRow(0, new[] {false, false})]
		[DataRow(1, new[] {true, false})]
		[DataRow(2, new[] {false, true})]
		[DataRow(3, new[] {true, true})]
		public void CorrectInputGoesToOutput(int inputNumber, bool[] selectInput)
		{
			var mux = new Multiplexer(4, 2);
			SetMuxInputs(mux);
			Util.SetArrayToValues(mux.InputSelect, selectInput);
			Assert.IsTrue(mux.Tick());
			CollectionAssert.AreEqual(m_inputs[inputNumber], mux.Output);
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
