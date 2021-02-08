using System;
using System.Linq;

namespace GateSim.InputOutputs
{
	public class ConstantOutput : IDevice
	{
		public bool[] m_output { get; set; }
		public int Id { get; set; }

		public int Result = 0;
		public bool ConstantValue;
		public int BitWidth;

		public bool Tick()
		{
			return false;
		}

		public ConstantOutput(int bitWidth, bool constantValue)
		{
			BitWidth = bitWidth;
			ConstantValue = constantValue;
			m_output = Enumerable.Repeat(ConstantValue, bitWidth).ToArray();
		}

		public bool[] GetOutput()
		{
			return m_output;
		}

		//Deep copy the values in newOutput into m_output.
		public void SetOutputDeep(bool[] newOutput)
		{
			if (newOutput.Length != m_output.Length)
			{
				throw new Exception("newOutput is the wrong width");
			}

			for (int i = 0; i < m_output.Length; i++)
			{
				m_output[i] = newOutput[i];
			}
		}

		public void SetSpecificBit(int bitNum, bool newValue)
		{
			m_output[bitNum] = newValue;
		}

	}
}
