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
		//key is the wire's input array. This lets us see if there's already a wire
		//attached to that array and add a new output if there is, or create
		//a new wire if there isn't
		private readonly Dictionary<bool[], Wire> m_wires = new Dictionary<bool[], Wire>();
		private readonly List<IDevice> m_devices = new List<IDevice>();

		//This lets something external access individual devices in the sim.
		//Ex. An interface wants to display the state of specific devices.
		private readonly Dictionary<string, IDevice> m_devicesById = new Dictionary<string, IDevice>();

		//If a wire doesn't exist with it's input as deviceOutput, create it
		//Add the deviceInput as an output for the wire.
		//Ensure the wire is in the simulation's collection of wires.
		public Wire Connect(bool[] deviceOutput, params bool[][] deviceInputs)
		{
			if(!m_wires.ContainsKey(deviceOutput)){
				var wire = new Wire(deviceOutput);
				m_wires.Add(deviceOutput, wire);
			}

			foreach(var deviceInput in deviceInputs){
				m_wires[deviceOutput].AddOutput(deviceInput);
			}

			return m_wires[deviceOutput];
		}

		public T AddDevice<T>(T device, string ID = "") where T : IDevice
		{
			m_devices.Add(device);
			if(ID != "")
			{
				m_devicesById.Add(ID, device);
			}
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

		public IDevice GetDevice(string ID){
			return m_devicesById[ID];
		}

	}
}
