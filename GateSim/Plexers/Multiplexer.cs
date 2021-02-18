using System;
using System.Linq;

namespace GateSim.Plexers
{
	public class Multiplexer : IDevice
	{
		private readonly bool[][] m_inputs;
		public bool[] Output { get; }

		/// <summary>
		/// The least significant digit is index 0. {true, false, false} selects input 1 not 4.
		/// </summary>
		public bool[] InputSelect { get; }

		public int Id { get; set; }
		public bool Tick()
		{
			var oldState = (bool[])Output.Clone();

			var chosenInput = InputSelect.ToInt();

			for (int i = 0; i < Output.Length; i++)
			{
				Output[i] = m_inputs[chosenInput][i];
			}

			return !oldState.SequenceEqual(Output);
		}

		public Multiplexer(int bitWidth, int numSelectBits)
		{
			InputSelect = new bool[numSelectBits];
			var numInputs = (int) Math.Pow(2, numSelectBits);

			m_inputs = new bool[numInputs][];
			for (int i = 0; i < numInputs; i++)
			{
				m_inputs[i] = new bool[bitWidth];
			}

			Output = new bool[bitWidth];
		}

		public bool[] GetInput(int index)
		{
			return m_inputs[index];
		}

	}
}
