using System;
using System.Collections.Generic;

namespace GateSim.Wiring
{
	/// <summary>
	/// Wires are really just devices that when they Tick, copy their
	/// input to their outputs without changing it. However, for a simulation
	/// to be deterministic, all the actual devices need to tick before
	/// any wires. (For example, if a device ticks after the wire that's
	/// connected to it's output, the wire won't move the new output along
	/// until the next "round".) Wire doesn't implement IDevice in order
	/// to enforce this.
	/// </summary>
	public class Wire
	{
		public int BitWidth { get; }

		private readonly bool[] m_input;

		private readonly List<bool[]> m_outputs = new List<bool[]>();

		public Wire(bool[] input)
		{
			BitWidth = input.Length;
			m_input = new bool[BitWidth];
			m_input = input;
		}

		public Wire(bool[] wireInput, bool[] wireOutput)
		{
			BitWidth = wireInput.Length;
			m_input = new bool[BitWidth];
			m_input = wireInput;

			AddOutput(wireOutput);
		}

		public void AddOutput(bool[] output)
		{
			if (output.Length != BitWidth)
			{
				throw new Exception("output width doesn't match input width");
			}
			m_outputs.Add(output);
		}

		public void CopyInputToOutput()
		{
			foreach (var output in m_outputs)
			{
				for (int i = 0; i < BitWidth; i++)
				{
					output[i] = m_input[i];
				}
			}
		}

	}
}
