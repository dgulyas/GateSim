using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class XorGate : IDevice
	{
		public List<bool[]> Inputs { get; set; }
		public bool[] Output { get; set; }
		public int Id { get; set; }

		private readonly int m_bitWidth;

		public bool Tick()
		{
			//xor is true if there's an odd number of true inputs
			var oldState = (bool[])Output.Clone();
			for (int i = 0; i < m_bitWidth; i++)
			{
				Output[i] = false;
				foreach (var input in Inputs)
				{
					if (input[i])
					{
						Output[i] = !Output[i];
					}
				}
			}

			return !oldState.SequenceEqual(Output);
		}

		public XorGate(int bitWidth)
		{
			m_bitWidth = bitWidth;
			Inputs = new List<bool[]>();
			Output = new bool[m_bitWidth];
		}

		public bool[] GetNewInput()
		{
			var input = new bool[m_bitWidth];
			Inputs.Add(input);
			return input;
		}

		public bool[] GetOutput()
		{
			return Output;
		}

	}
}
