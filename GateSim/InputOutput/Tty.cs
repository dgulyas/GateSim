using System;

namespace GateSim.InputOutput
{
	public class Tty : IDevice
	{
		public bool[] Input { get; }
		public bool[] Enable { get; }
		public bool[] Clock { get; }
		private readonly bool[] m_clockAtPreviousTick;

		private readonly Action<char> m_writeAction;


		public int Id { get; set; }
		public bool Tick()
		{
			if (Enable[0] && !m_clockAtPreviousTick[0] && Clock[0]) //if the clock has a rising edge
			{
				var inputChar = (char)Input.ToInt();
				m_writeAction(inputChar);
			}
			return false;
		}

        public string GetStateString()
        {
            throw new NotImplementedException();
        }

        //We have two constructors so we can override the output method for testing
        public Tty(int bitWidth) : this(bitWidth, Console.Write) { }

		public Tty(int bitWidth, Action<char> writeAction)
		{
			if (bitWidth < 8)
			{
				throw new Exception("TTY input needs a bit width of 8 or larger");
			}

			Input = new bool[bitWidth];
			Enable = new bool[1];
			Clock = new bool[1];
			m_clockAtPreviousTick = new bool[1];

			m_writeAction = writeAction;
		}
	}
}
