namespace GateSim.Arithmetic
{
    public class Adder : IDevice
    {
        public bool[] Input1 { get; }
        public bool[] Input2 { get; }
        public bool[] Output { get; }
        public int Id { get; set; }

        public bool Tick()
        { //This isn't a great implementation. There are probably a lot
          //of edge cases around converting to int and back again.
          //Does this work for 2's complement negatives?
            var oldState = (bool[]) Output.Clone();

            var a = Input1.ToInt();
            var b = Input2.ToInt();

            var c = a+b;
            if(c > Math.Pow(2,Output.Length)){ //Is this right?
                c = (int)Math.Pow(2,Output.Length); //It might need to be "- 1"
            }

            Util.SetArrayToValues(Output, c.ToBoolArray(Output.Length));

            return !oldState.SequenceEqual(Output);
        }

        public Adder(int bitWidth){
            Input1 = new bool[bitWidth];
            Input2 = new bool[bitWidth];
            Output = new bool[bitWidth];
        }
    }
}