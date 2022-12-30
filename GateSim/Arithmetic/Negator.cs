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
                AddOne(Output);
            }
            else //currently negative
            {
                MinusOne(Output);
                Output.Invert();
            }

            return !oldState.SequenceEqual(Output);
        }

        public static void AddOne(bool[] a){
            for(int i = 0; i < a.Length; i++){
                if(a[i] == false){
                    a[i] = true;
                    break;
                }
                else{
                    a[i] = false;
                }
            }
        }

        public static void MinusOne(bool[] a){
            for(int i = 0; i < a.Length; i++){
                if(a[i] == false){
                    a[i] = true;
                }
                else{
                    a[i] = false;
                    break;
                }
            }
        }

        public Negator(int bitwidth){
            Input = new bool[bitwidth];
            Output = new bool[bitwidth];
        }
    }
}