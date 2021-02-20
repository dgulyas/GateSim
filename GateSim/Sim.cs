using System;
using System.Collections.Generic;
using GateSim.Wiring;

namespace GateSim
{
	public class Sim
	{
		private readonly Dictionary<bool[], Wire> m_wires = new Dictionary<bool[], Wire>();
		private readonly List<IDevice> m_devices = new List<IDevice>();

		public Wire Connect(bool[] feederArray, bool[] eaterArray)
		{
			if (m_wires.ContainsKey(feederArray))
			{
				m_wires[feederArray].AddOutput(eaterArray);
				return m_wires[feederArray];
			}
			else
			{
				var wire = new Wire(feederArray, eaterArray);
				m_wires.Add(feederArray, wire);
				return wire;
			}
		}

		public T CreateDevice<T>(T device) where T : IDevice
		{
			m_devices.Add(device);
			return device;
		}

		//The state is "settled" when none of the outputs change.
		//That means everything should have propogated through the system.
		public void SettleState()
		{
			var somethingChanged = true;
			var numCycles = 0;

			while (somethingChanged && numCycles < 1000)
			{
				somethingChanged = false;

				foreach (var wire in m_wires.Values)
				{
					wire.CopyInputToOutput();
				}

				foreach (var device in m_devices)
				{
					if (device.Tick())
					{
						somethingChanged = true;
					}
				}

				numCycles++;
			}

			if (numCycles >= 1000)
			{ //It might have settled on the 1000th cycle
				throw new Exception("State isn't settling");
			}
		}

	}
}
