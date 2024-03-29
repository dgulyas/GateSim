﻿namespace GateSim.Wiring
{
	public class ConstantOutput : IDevice
	{
		public bool[] Output { get; }
		public int Id { get; set; }

		public bool Tick(bool printDebug = false)
		{
			return false;
		}

		public ConstantOutput(int bitWidth)
		{
			Output = new bool[bitWidth];
		}

		public ConstantOutput(int bitWidth, bool constantValue)
		{
			Output = Enumerable.Repeat(constantValue, bitWidth).ToArray();
		}

		public ConstantOutput(bool[] startingValue)
		{
			Output = new bool[startingValue.Length];
			Util.SetArrayToValues(Output, startingValue);
		}

		public void SetSpecificBit(int bitNum, bool newValue)
		{
			Output[bitNum] = newValue;
		}

		public string GetStateString()
		{
			return Output.ToInt().ToString() + ":" + Output.ArrayToString();
		}
	}
}
