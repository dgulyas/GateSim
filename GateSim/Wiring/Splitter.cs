using System;
using System.Collections.Generic;
using System.Linq;

namespace GateSim.Wiring
{
	public class Splitter : IDevice
	{
		public bool[] Input { get; }
		private bool[][] m_outputs;

		//Dict key specifies which output. Dict value is the inclusive
		//range of the input that should be coped to that output
		//The below example splits an 8 bit input in half into 2 outputs:
		//Key 0, Value (0,3)
		//Key 1, Value (4,7)
		private Dictionary<int, Tuple<int, int>> m_mapping;

		public int Id { get; set; }
		public bool Tick()
		{
			var outputsChanged = false;
			foreach (var key in m_mapping.Keys)
			{
				var oldState = (bool[])m_outputs[key].Clone();

				var sectionStart = m_mapping[key].Item1;
				var sectionEnd = m_mapping[key].Item2;
				var sectionLength = sectionEnd - sectionStart + 1; //Section includes endpoints so add 1
				for (int i = 0; i < sectionLength; i++)
				{
					m_outputs[key][i] = Input[i + sectionStart];
				}

				if (!oldState.SequenceEqual(m_outputs[key]))
				{
					outputsChanged = true;
				}
			}

			return outputsChanged;
		}

		public Splitter(int bitWidth, Dictionary<int, Tuple<int, int>> mapping)
		{
			Input = new bool[bitWidth];
			foreach (var value in mapping.Values)
			{
				if (value.Item1 > value.Item2)
				{
					throw new Exception("The smaller number should come first in the tuple");
				}

				if (value.Item2 > bitWidth + 1)
				{
					throw new Exception("Mapping tries to map bits beyond end of input");
				}
			}

			m_mapping = DeepCopyMapping(mapping);
			var numOutputs = mapping.Keys.Max() + 1;
			m_outputs = new bool[numOutputs][];
			foreach (var key in m_mapping.Keys)
			{
				var sectionSize = m_mapping[key].Item2 - m_mapping[key].Item1 + 1;
				m_outputs[key] = new bool[sectionSize];
			}
		}

		public bool[] GetOutput(int index)
		{
			return m_outputs[index];
		}

		//All the objects in this class's mapping shouldn't be refrenced by other
		//objects, so we create an entirely new mapping using the primatives in the passed
		//in mapping
		private Dictionary<int, Tuple<int, int>> DeepCopyMapping(Dictionary<int, Tuple<int, int>> mapping)
		{
			var newMapping = new Dictionary<int, Tuple<int, int>>();
			foreach (var key in mapping.Keys)
			{
				newMapping.Add(key, new Tuple<int, int>(mapping[key].Item1, mapping[key].Item2));
			}

			return newMapping;
		}
	}
}
