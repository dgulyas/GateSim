using GateSim;

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
		//Each device can return a string[] containing it's state.
		//Each frame The dictionary is iterated through, getting the device's state and
		//displaying it at the tuple coordinates.
		private Dictionary<IDevice, Tuple<int, int>> deviceCoords;

		//Each list item is a string and the coordinates it will be displayed at.
		private List<Tuple<string, Tuple<int, int>>> stringCoords; //aka labels.
		private int m_height;
		private int m_width;
		private char[][] buffer;

		//A group of input and output arrays, and the coordinates they'll be displayed at.
		//This makes it easy to display a devices inputs and outputs.
        private Dictionary<bool[], Tuple<int, int>> ioCords;

		public Interface(int height, int width){
			deviceCoords = new Dictionary<IDevice, Tuple<int, int>>();
			stringCoords = new List<Tuple<string, Tuple<int, int>>>(); 
            ioCords = new Dictionary<bool[], Tuple<int, int>>();
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

			var newLabel = new Tuple<string, Tuple<int, int>>(str, new Tuple<int, int>(x, y));

			stringCoords.Add(newLabel);
		}

        public void AddIO(bool[] io, int x, int y)
        {
            if (!ContainsCoords(x, y))
            {
                throw new Exception("Coordinates are outside buffer.");
            }

			ioCords.Add(io, new Tuple<int, int>(x, y));
        }

		public string[] RefreshScreen(){
			ClearBuffer();
			FillBuffer();

            var strings = new string[buffer.Length];

            for (int i = 0; i < buffer.Length; i++)
            {
                strings[i] = new string(buffer[i]);
            }

            return strings;
        }

		private void FillBuffer(){
			//Copies the device states, and strings, ect into the buffer.

			foreach(var deviceCoord in deviceCoords)
			{
				WriteStringToCoord(deviceCoord.Key.GetStateString(), deviceCoord.Value);
			}

			foreach(var lbl in stringCoords)
			{
				WriteStringToCoord(lbl.Item1, lbl.Item2);
			}

            foreach (var ioCord in ioCords)
            {
				WriteStringToCoord(ioCord.Key.ArrayToString(), ioCord.Value);
            }
		}

        private void WriteStringToCoord(string str, Tuple<int, int> coords)
        {
            var strX = coords.Item1;
            var strY = coords.Item2;

            CopyStringIntoBuffer(str, strX, strY);
        }

		private void CopyStringIntoBuffer(string str, int x, int y){
			for(int i = 0; i < str.Length; i++){
					var charX = i + x;
					var charY = y;

					if(!ContainsCoords(charX, charY)){
						break;
					}

					buffer[charY][charX] = str[i];
            }
		}

		private bool ContainsCoords(int x, int y){
			return x >= 0 && x < m_width && y >= 0 && y < m_height;
		}

		private void ClearBuffer()
        {
            foreach (var array in buffer)
            {
                for(int j = 0; j < array.Length; j++){
                    array[j] = ' ';
                }
            }
        }
	}
}