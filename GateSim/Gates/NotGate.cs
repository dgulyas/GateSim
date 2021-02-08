using System.Linq;

namespace GateSim.Gates
{
	public class NotGate : IDevice
	{
		public bool[] Input { get; set; }
		public bool[] Output { get; set; }
		public int Id { get; set; }

		private readonly int m_bitWidth;

		public bool Tick()
		{
			var oldState = (bool[])Output.Clone();
			for (int i = 0; i < m_bitWidth; i++)
			{
				Output[i] = !Input[i];
			}

			return !oldState.SequenceEqual(Output);
		}

		public NotGate(int bitWidth)
		{
			m_bitWidth = bitWidth;
			Input = new bool[bitWidth];
			Output = new bool[m_bitWidth];
		}

		public bool[] GetInput()
		{
			return Input;
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
