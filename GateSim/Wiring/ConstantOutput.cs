using System;
using System.Linq;

namespace GateSim.Wiring
{
	public class ConstantOutput : IDevice
	{
		public bool[] Output { get; }
		public int Id { get; set; }

		public bool Tick()
		{
			return false;
		}

		public ConstantOutput(int bitWidth, bool constantValue)
		{
			Output = Enumerable.Repeat(constantValue, bitWidth).ToArray();
		}

		public ConstantOutput(bool[] startingValue)
		{
			Output = new bool[startingValue.Length];
			SetOutputDeep(startingValue);
		}

		//Deep copy the values in newOutput into Output.
		public void SetOutputDeep(bool[] newOutput)
		{
			if (newOutput.Length != Output.Length)
			{
				throw new Exception("newOutput is the wrong width");
			}

			for (int i = 0; i < Output.Length; i++)
			{
				Output[i] = newOutput[i];
			}
		}

		public void SetSpecificBit(int bitNum, bool newValue)
		{
			Output[bitNum] = newValue;
		}

	}
}
