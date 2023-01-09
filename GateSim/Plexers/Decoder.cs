using System;

namespace GateSim.Plexers
{
	public class Decoder : IDevice
	{
		public bool[] OutputSelect { get; }
		private readonly bool[][] m_outputs;
		public int Id { get; set; }

		public bool Tick(bool printDebug = false)
		{
			var selectedOutputIndex = OutputSelect.ToInt();
			var outputChanging = !m_outputs[selectedOutputIndex][0]; //If the selected output isn't true, it needs to change

			if (outputChanging)
			{
				SetSelectedOutTrueOthersFalse(selectedOutputIndex);
			}

			return outputChanging;
		}

		public Decoder(int numSelectBits)
		{
			OutputSelect = new bool[numSelectBits];
			var numOutputs = (int) Math.Pow(2, numSelectBits);
			m_outputs = new bool[numOutputs][];
			for (int i = 0; i < m_outputs.Length; i++)
			{
				m_outputs[i] = new bool[1];
			}

			//Set initial state
			var outputActive = OutputSelect.ToInt();
			SetSelectedOutTrueOthersFalse(outputActive);
		}

		public bool[] GetOutput(int index)
		{
			return m_outputs[index];
		}

		private void SetSelectedOutTrueOthersFalse(int selectedOutput)
		{
			foreach (var x in m_outputs)
			{
				x[0] = false;
			}
			m_outputs[selectedOutput][0] = true;
		}

		public string GetStateString()
		{
			throw new NotImplementedException();
		}
	}
}
