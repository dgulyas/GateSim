using GateSim.Arithmetic;
using GateSim.InputOutput;
using GateSim.Memory;
using GateSim.Plexers;
using GateSim.Wiring;

namespace GateSim.Simulations
{
    public class Cpu01
    {
        public const int BitWidth = 8;
        private Sim sim = new Sim();

        public bool[] RegSelect;
        public bool[] Arg1Select;
        public bool[] Arg2Select;
        public bool[] FuncSelect;
        public bool[] Literal;
        public bool[] Clock;

        public Cpu01()
        {
            var deco1 = sim.AddDevice(new Decoder(2), "deco1");

            var r0 = sim.AddDevice(new Register(BitWidth), "r0");
            var r1 = sim.AddDevice(new Register(BitWidth), "r1");
            var r2 = sim.AddDevice(new Register(BitWidth), "r2");
            var r3 = sim.AddDevice(new Register(BitWidth), "r3");

            var mux1 = sim.AddDevice(new Multiplexer(BitWidth, 2), "mux1");
            var mux2 = sim.AddDevice(new Multiplexer(BitWidth, 2), "mux2");
            var mux3 = sim.AddDevice(new Multiplexer(BitWidth, 2), "mux3");

            var adder = sim.AddDevice(new Adder(BitWidth), "adder");
            var subtractor = sim.AddDevice(new Subtractor(BitWidth), "subtractor");
            var negator = sim.AddDevice(new Negator(BitWidth), "negator");
            var literal = sim.AddDevice(new ConstantOutput(BitWidth), "literal");

            var clk = sim.AddDevice(new Switch(), "clk");

            sim.Connect(deco1.GetOutput(0), r0.Enable);
            sim.Connect(deco1.GetOutput(1), r1.Enable);
            sim.Connect(deco1.GetOutput(2), r2.Enable);
            sim.Connect(deco1.GetOutput(3), r3.Enable);

            sim.Connect(r0.Output, mux1.GetInput(0), mux2.GetInput(0));
            sim.Connect(r1.Output, mux1.GetInput(1), mux2.GetInput(1));
            sim.Connect(r2.Output, mux1.GetInput(2), mux2.GetInput(2));
            sim.Connect(r3.Output, mux1.GetInput(3), mux2.GetInput(3));

            sim.Connect(mux1.Output, adder.Input1, subtractor.Input1, negator.Input);
            sim.Connect(mux2.Output, adder.Input2, subtractor.Input2);

            sim.Connect(adder.Output, mux3.GetInput(0));
            sim.Connect(subtractor.Output, mux3.GetInput(1));
            sim.Connect(negator.Output, mux3.GetInput(2));
            sim.Connect(literal.Output, mux3.GetInput(3));

            sim.Connect(mux3.Output, r0.Input, r1.Input, r2.Input, r3.Input);

            sim.Connect(clk.Output, r0.Clock, r1.Clock, r2.Clock, r3.Clock);

            RegSelect = deco1.OutputSelect;
            Arg1Select = mux1.InputSelect;
            Arg2Select = mux2.InputSelect;
            FuncSelect = mux3.InputSelect;
            Literal = literal.Output;
            Clock = clk.Output;
        }

        public void SettleState()
        {
            sim.SettleState();
        }

        public IDevice GetDevice(string ID){
            return sim.GetDevice(ID);
        }
    }
}