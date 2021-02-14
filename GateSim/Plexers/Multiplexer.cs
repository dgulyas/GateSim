using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateSim.Plexers
{
	public class Multiplexer : IDevice
	{
		private readonly bool[][] m_inputs;
		public bool[] Output { get; }
		public bool[] InputSelect { get; }


		public int Id { get; set; }
		public bool Tick()
		{
			var oldState = (bool[])Output.Clone();

			var chosenInput = ConvertBoolArrayToInt(InputSelect);

			for (int i = 0; i < Output.Length; i++)
			{
				Output[i] = m_inputs[chosenInput][i];
			}

			return !oldState.SequenceEqual(Output);
		}

		public Multiplexer(int bitWidth, int numSelectBits)
		{
			InputSelect = new bool[numSelectBits];
			var numInputs = (int) Math.Pow(2, numSelectBits);

			m_inputs = new bool[numInputs][];
			for (int i = 0; i < numInputs; i++)
			{
				m_inputs[i] = new bool[bitWidth];
			}

			Output = new bool[bitWidth];
		}

		public bool[] GetInput(int index)
		{
			return m_inputs[index];
		}

		private static int ConvertBoolArrayToInt(bool[] array)
		{
			var workingTotal = 0;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i])
				{
					workingTotal += (int) Math.Pow(2, i);
				}
			}

			return workingTotal;
		}
	}
}
