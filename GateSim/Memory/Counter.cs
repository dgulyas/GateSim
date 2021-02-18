using System;
using System.Linq;

namespace GateSim.Memory
{
	public class Counter : IDevice
	{
		public bool[] Output { get; }
		public bool[] Input { get; }
		public bool[] Load { get; }
		public bool[] Clear { get; }
		public bool[] Clock { get; }
		private bool[] m_clockAtPreviousTick;

		public int Id { get; set; }
		public bool Tick()
		{
			var oldState = (bool[])Output.Clone();
			if (Clear[0])
			{
				Array.Clear(Output, 0, Output.Length);
			}
			else if(!m_clockAtPreviousTick[0] && Clock[0]) //If the clock has a rising edge
			{
				if (Load[0])
				{
					Util.SetArrayToValues(Output,Input);
				}
				else
				{
					//This isn't an efficent way to do this, but it's simple.
					var incrementedArray = (Output.ToInt() + 1).ToBoolArray(Output.Length);
					Util.SetArrayToValues(Output, incrementedArray);
				}
			}

			m_clockAtPreviousTick = (bool[])Clock.Clone();
			return !oldState.SequenceEqual(Output);
		}

		public Counter(int bitWidth)
		{
			Output = new bool[bitWidth];
			Input = new bool[bitWidth];
			Load = new bool[1];
			Clear = new bool[1];
			Clock = new bool[1];
			m_clockAtPreviousTick = new bool[1];
		}

	}
}
