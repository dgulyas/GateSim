using GateSim;
using System.Collections.Generic;

namespace Terminal
{
	/// <summary>
	/// This class maintains a region of terminal screen space.
	/// IDevices are registered at certain locations with AddDevice().
	/// Calling GetCurrentState() returns the state of all registered
	/// devices arranged at their coordinates. It's returned as a string[]
	/// which lets it be easily written to the terminal.
	///
	/// The intent is that a Simulation is created with all relevant devices
	/// added to an Interface instance. After every simulation tick, the current
	/// state is printed to the terminal, and the user can check it, or in the future,
	/// alter the inputs via terminal command, which will let the simulations
	/// be interactive.
	/// </summary>
	public class Interface
	{
		private Dictionary<IDevice, Tuple<int, int>> deviceCoords;
		private Dictionary<string, Tuple<int, int>> stringCoords; //aka labels.
		private int m_height;
		private int m_width;
		private char[][] buffer;

		public Interface(int height, int width){
			deviceCoords = new Dictionary<IDevice, Tuple<int, int>>();
			stringCoords = new Dictionary<string, Tuple<int, int>>();
			m_height = height;
			m_width = width;

			buffer = new char[height][];
			for(int i = 0; i < buffer.Length; i++){
				buffer[i] = new char[width];
			}
		}

		//Only the first character is guaranteed to be inside the screen.
		//It might run farther than the screen.
		public void AddDevice(IDevice device, int x, int y ){
			if(!ContainsCoords(x,y)){
				throw new Exception("Coordinates are outside buffer.");
			}

			deviceCoords.Add(device, new Tuple<int, int>(x, y));
		}

		public void AddString(string str, int x, int y){
			if(!ContainsCoords(x,y)){
				throw new Exception("Coordinates are outside buffer.");
			}

			stringCoords.Add(str, new Tuple<int, int>(x, y));
		}

		public void Refresh(){
			ClearBuffer();
			FillBuffer();
		}

		public string[] GetBufferState()
		{
			var strings = new string[buffer.Length];

			for(int i = 0; i < buffer.Length; i++){
				strings[i] = new string(buffer[i]);
			}

			return strings;
		}

		public string[] GetCurrentState(){
			Refresh();
			return GetBufferState();
		}

		private void FillBuffer(){
			//Copies the device states and strings into the buffer.

			foreach(var device in deviceCoords.Keys)
			{
				var deviceX = deviceCoords[device].Item1;
				var deviceY = deviceCoords[device].Item2;
				var stateString = device.GetStateString();

				CopyStringIntoBuffer(stateString, deviceX, deviceY);
			}

			foreach(var str in stringCoords.Keys)
			{
				var strX = stringCoords[str].Item1;
				var strY = stringCoords[str].Item2;
				CopyStringIntoBuffer(str, strX, strY);
			}
		}

		private void CopyStringIntoBuffer(string str, int x, int y){
			for(int i = 0; i < str.Length; i++){
					var charX = i + x;
					var charY = y;

					if(!ContainsCoords(charX, charY)){
						break;
					}

					buffer[charX][charY] = str[i];
				}
		}

		private bool ContainsCoords(int x, int y){
			return x >= 0 && x < m_width && y >= 0 && y < m_height;
		}

		private void ClearBuffer(){
			for(int i = 0; i < buffer.Length; i++)
			{
				for(int j = 0; j < buffer[i].Length; j++){
					buffer[i][j] = ' ';
				}
			}
		}
	}
}