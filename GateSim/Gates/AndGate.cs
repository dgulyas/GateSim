using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class AndGate : IDevice
	{
		public List<bool[]> Inputs { get; set; }
		public bool[] Output { get; set; }
		public int Id { get; set; }

		private readonly int m_bitWidth;

		public bool Tick()
		{
			var oldState = (bool[]) Output.Clone();
			for(int i = 0; i < m_bitWidth; i++)
			{
				Output[i] = true;
				foreach (var input in Inputs)
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

		public string GetState()
		{
			var state = "";
			for (int i = 0; i < m_bitWidth; i++)
			{
				state += Output[i] ? "1" : "0";
			}

			return state;
		}
	}
}
