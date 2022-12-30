namespace GateSim.Arithmetic
{
    public class Adder : IDevice
    {
        public bool[] Input1 { get; }
        public bool[] Input2 { get; }
        //public bool[] CarryIn {get; }
        public bool[] CarryOut { get; }
        public bool[] Output { get; }
        public int Id { get; set; }

        public bool Tick(){
            var oldState = (bool[]) Output.Clone();

            var sumArray = new bool[Input1.Length];
            bool carry = false;
            for(int i = 0; i < sumArray.Length; i++)
            {
                var sum = (carry ? 1: 0) + (Input1[i] ? 1 : 0) + (Input2[i] ? 1 : 0);
                if(sum == 0){
                    sumArray[i] = false;
                    carry = false;
                }
                else if(sum == 1)
                {
                    sumArray[i] = true;
                    carry = false;
                }
                else if(sum == 2)
                {
                    sumArray[i] = false;
                    carry = true;
                }
                else if(sum == 3)
                {
                    sumArray[i] = true;
                    carry = true;
                }
            }

            CarryOut[0] = carry;
            Util.SetArrayToValues(Output, sumArray);

            return !oldState.SequenceEqual(Output);
        }

        public Adder(int bitWidth){
            Input1 = new bool[bitWidth];
            Input2 = new bool[bitWidth];
            Output = new bool[bitWidth];
            CarryOut = new bool[1];
        }
    }
}