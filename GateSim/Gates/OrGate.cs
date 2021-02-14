using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class OrGate : IDevice
	{
		private readonly List<bool[]> m_inputs;
		public bool[] m_output { get; }
		public int Id { get; set; }

		private readonly int m_bitWidth;

		public bool Tick()
		{
			var oldState = (bool[])m_output.Clone();
			for (int i = 0; i < m_bitWidth; i++)
			{
				m_output[i] = false;
				foreach (var input in m_inputs)
				{
					if (input[i])
					{
						m_output[i] = true;
					}
				}
			}

			return !oldState.SequenceEqual(m_output);
		}

		public OrGate(int bitWidth)
		{
			m_bitWidth = bitWidth;
			m_inputs = new List<bool[]>();
			m_output = new bool[m_bitWidth];
		}

		public bool[] GetNewInput()
		{
			var input = new bool[m_bitWidth];
			m_inputs.Add(input);
			return input;
		}

	}
}
