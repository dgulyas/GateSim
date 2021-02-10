using System.Collections.Generic;
using System.Linq;

namespace GateSim.Gates
{
	public class AndGate : IDevice
	{
		private List<bool[]> Inputs;
		public bool[] Output { get; }
		public int Id { get; set; }

		public bool Tick()
		{
			var oldState = (bool[]) Output.Clone();
			for(int i = 0; i < Output.Length; i++)
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
			Inputs = new List<bool[]>();
			Output = new bool[bitWidth];
		}

		public bool[] GetNewInput()
		{
			var input = new bool[Output.Length];
			Inputs.Add(input);
			return input;
		}

		public string GetState()
		{
			var state = "";
			foreach (var x in Output)
			{
				state += x ? "1" : "0";
			}

			return state;
		}
	}
}
