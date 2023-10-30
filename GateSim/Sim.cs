using GateSim.Wiring;

namespace GateSim
{
	//How to use a Sim:
	// Setup:
	//1: Create all the devices you need using CreateDevice(), and save
	//	 the refrences to the devices that are returned so you can connect
	//	 them to each other.
	//2: Connect the outputs and inputs of those devices using Connect().
	//   You can use or ignore the Wire that Connect returns. Ideally ignore it.
	// Runtime:
	//3: Using the devices you saved, modify their inputs.
	//4: Call SettleState() to have the sim reach the next stable state
	//   where every change has propagated through the sim.
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

		//A device's output is a wire's input and vice versa.
		//If a wire doesn't exist with it's input as deviceOutput, create it.
		//Add the deviceInputs as outputs for the wire.
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
		//Step 1: Copy the every wire's input the that wire's outputs
		//Step 2: Tick each device now that their inputs might have changed.
		//Step 3: Repeat the above steps until no device outputs change or
		//        1000 iterations ahve been done.
		//Step 4: If it's been more than 1000 iterations, and things are still
		//        changing, throw an exception.
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
