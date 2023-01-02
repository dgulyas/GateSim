namespace GateSim.InputOutput
{
	public class Switch : IDevice
	{
		public bool[] Output { get; }
		public int Id { get; set; }

		public bool Tick()
		{
			return false;
		}

		public Switch(bool startingValue = false)
		{
			Output = new bool[]{startingValue};
		}

        public void Set(bool value){
            Output[0] = value;
        }

        public void Toggle(){
            Output[0] = !Output[0];
        }

        public string GetStateString()
        {
            throw new NotImplementedException();
        }
    }
}
