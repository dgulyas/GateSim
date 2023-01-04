
namespace GateSim
{
	public interface IDevice
	{
		int Id { get; set; }

		//This is where the device takes what's in it's inputs, modifies it,
		//and loads it into it's outputs.
		//Returns a bool to indicate of the outputs changed. This tells us if
		//it's reached a steady state. If all devices return false we can stop
		//iterating over the devices, since more iterations won't change anything.
		//It's possible for stable state to never be never reached.
		//ex. A negator feeds into itself
		bool Tick(bool printDebug = false);

		string GetStateString();

		//A device will also have functions that return it's input and output bool arrays.
		//These are different for every device so it's messy to try and encapsulate them in
		//an interface.
	}

}
