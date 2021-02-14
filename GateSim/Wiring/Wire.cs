using System;
using System.Collections.Generic;

namespace GateSim
{
	public class Wire
	{
		public int BitWidth { get; }

		private readonly bool[] m_input;

		private readonly List<bool[]> m_outputs = new List<bool[]>();

		public Wire(bool[] input)
		{
			BitWidth = input.Length;
			m_input = new bool[BitWidth];
			m_input = input;
		}

		public Wire(bool[] input, bool[] output)
		{
			BitWidth = input.Length;
			m_input = new bool[BitWidth];
			m_input = input;

			AddOutput(output);
		}

		public void AddOutput(bool[] output)
		{
			if (output.Length != BitWidth)
			{
				throw new Exception("output width doesn't match input width");
			}
			m_outputs.Add(output);
		}

		public void CopyInputToOutput()
		{
			foreach (var output in m_outputs)
			{
				for (int i = 0; i < BitWidth; i++)
				{
					output[i] = m_input[i];
				}
			}
		}

	}
}
