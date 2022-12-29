using GateSim.Wiring;

namespace GateSim
{
	//How to use a Sim:
	//1: Create all the devices you need using CreateDevice().
	//2: Connect the outputs and inputs of those devices using Connect().
	//   You can use or ignore the Wire that connect returns. Ideally ignore it.
	//3: Repeatedly call SettleState() to have the sim reach the next stable state
	//   where every change has propagated through the system.
	public class Sim
	{
		private readonly Dictionary<bool[], Wire> m_wires = new Dictionary<bool[], Wire>();
		private readonly List<IDevice> m_devices = new List<IDevice>();

		//If a wire doesn't exist with it's input as feederArray, create it
		//Add the eaterArray as an output for the wire.
		//Ensure the wire is in the simulation's collection of wires.
		public Wire Connect(bool[] feederArray, params bool[][] eaterArrays)
		{
			if(!m_wires.ContainsKey(feederArray)){
				var wire = new Wire(feederArray);
				m_wires.Add(feederArray, wire);
			}

			foreach(var eaterArray in eaterArrays){
				m_wires[feederArray].AddOutput(eaterArray);
			}

			return m_wires[feederArray];
		}

		public T CreateDevice<T>(T device) where T : IDevice
		{
			m_devices.Add(device);
			return device;
		}

		//The state is "settled" when none of the outputs change.
		//That means everything should have propagated through the system.
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
					somethingChanged |= device.Tick();
				}

				numCycles++;
			}

			if (numCycles >= 1000 || somethingChanged)
			{ //I think this 'if' is correct?
				throw new Exception("State isn't settling");
			}
		}

	}
}
