using System;
using System.Linq;

namespace GateSim.Memory
{
	public class Register : IDevice
	{
		public bool[] Input { get; }
		public bool[] Output { get; }
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
			else if (!m_clockAtPreviousTick[0] && Clock[0]) //if the clock has a rising edge
			{
				for (int i = 0; i < Output.Length; i++)
				{
					Output[i] = Input[i];
				}
			}
			m_clockAtPreviousTick = (bool[])Clock.Clone();
			return !oldState.SequenceEqual(Output);
		}

		public Register(int bitWidth)
		{
			Input = new bool[bitWidth];
			Output = new bool[bitWidth];
			Clear = new bool[1];
			Clock = new bool[1];
			m_clockAtPreviousTick = new bool[1];
		}

	}
}
