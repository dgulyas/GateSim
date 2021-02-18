using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class OrGate : IDevice
	{
		private readonly List<bool[]> m_inputs;
		public bool[] Output { get; }
		public int Id { get; set; }

		private readonly int m_bitWidth;

		public bool Tick()
		{
			var oldState = (bool[])Output.Clone();
			for (int i = 0; i < m_bitWidth; i++)
			{
				Output[i] = false;
				foreach (var input in m_inputs)
				{
					if (input[i])
					{
						Output[i] = true;
					}
				}
			}

			return !oldState.SequenceEqual(Output);
		}

		public OrGate(int bitWidth)
		{
			m_bitWidth = bitWidth;
			m_inputs = new List<bool[]>();
			Output = new bool[m_bitWidth];
		}

		public bool[] GetNewInput()
		{
			var input = new bool[m_bitWidth];
			m_inputs.Add(input);
			return input;
		}

	}
}
