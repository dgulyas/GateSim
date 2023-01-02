namespace GateSim.Arithmetic
{
    public class Negator : IDevice
    {
        public bool[] Input;
        public bool[] Output;
        public int Id { get; set; }

        public bool Tick()
        {
            var oldState = (bool[]) Output.Clone();

            Util.SetArrayToValues(Output, Input);
            if(Output[Output.Length-1] == false){ //currently positive
                Output.Invert();
                Output.AddOne();
            }
            else //currently negative
            {
                Output.MinusOne();
                Output.Invert();
            }

            return !oldState.SequenceEqual(Output);
        }

        public string GetStateString()
        {
            throw new NotImplementedException();
        }

        public Negator(int bitwidth){
            Input = new bool[bitwidth];
            Output = new bool[bitwidth];
        }
    }
}