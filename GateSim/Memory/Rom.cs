using System.Linq;

namespace GateSim.Memory
{
    public class Rom : IDevice
    {
        //Determines which memory location is currently being output
        public bool[] Input { get; }

        //The value stored at memory location at index Input.
        public bool[] Output { get; }
        public bool[][] Contents { get; }

        /*
        dataBitWidth: the num of binary digits each of the memory locations has
        addressBitWidth: determines how many memory locations exist. 2^addressBitWidth
        */
        public Rom(int dataBitWidth, int addressBitWidth)
        {
            Input = new bool[addressBitWidth];
            Output = new bool[dataBitWidth];
            Contents = new bool[(int)Math.Pow(2, addressBitWidth)][];
        }

        public void Set(int address, bool[] data){
            //copy data array so the rom doesn't refer to the same values
            //this stops changes to data by the caller changing what's in the ROM.
            Contents[address] = data.ToArray();
        }

        //Is this even needed?
        //If it is, can the caller do the bool[] -> int conversion themselves?
        //public void Set(bool[] address, bool[] data){ }

        public int Id { get; set; }

        public bool Tick()
        {
            var oldState = (bool[])Output.Clone();

            var selectedData = Contents[Input.ToInt()];
            for(int i = 0; i < selectedData.Length; i++){
                Output[i] = selectedData[i];
            }
            return !oldState.SequenceEqual(Output);
        }

        /// <summary>
        /// Loads an integer array into the Rom.
        /// Length of array or Rom doesn't matter.
        /// </summary>
        /// <param name="data"></param>
        public void LoadFromArray(int[] data){
            for(int i = 0; i < data.Length && i < Contents.Length; i++){
                Contents[i] = data[i].ToBoolArray(Output.Length);
            }
        }

        public string GetStateString()
        {
            throw new NotImplementedException();
        }
    }
}