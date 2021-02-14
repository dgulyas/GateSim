using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class AndGate : IDevice
	{
		private readonly List<bool[]> m_inputs;
		public bool[] Output { get; }
		public int Id { get; set; }

		public bool Tick()
		{
			var oldState = (bool[]) Output.Clone();
			for(int i = 0; i < Output.Length; i++)
			{
				Output[i] = true;
				foreach (var input in m_inputs)
				{
					if (!input[i])
					{
						Output[i] = false;
					}
				}
			}

			return !oldState.SequenceEqual(Output);
		}

		public AndGate(int bitWidth)
		{
			m_inputs = new List<bool[]>();
			Output = new bool[bitWidth];
		}

		public bool[] GetNewInput()
		{
			var input = new bool[Output.Length];
			m_inputs.Add(input);
			return input;
		}

	}
}
